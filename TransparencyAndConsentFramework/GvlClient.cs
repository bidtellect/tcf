using Bidtellect.Tcf.Models;
using Bidtellect.Tcf.Models.Components.VendorList;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using static System.Text.Json.JsonElement;

namespace Bidtellect.Tcf
{
    /// <summary>
    /// Provides functionality to fetch the GVL from the web.
    /// </summary>
    public class GvlClient
    {
        protected const string vendorListUrl = "https://vendor-list.consensu.org/v2/vendor-list.json";

        /// <summary>
        /// Fetches the GVL from the web.
        /// </summary>
        /// <returns>
        /// A Global Vendor List object.
        /// </returns>
        public VendorList Fetch()
        {
            return Fetch(vendorListUrl);
        }

        /// <summary>
        /// fetches the GVL from the given URL.
        /// </summary>
        /// <param name="url">The URL from which to get the GVL JSON.</param>
        /// <returns>
        /// A Global Vendor List object.
        /// </returns>
        public VendorList Fetch(string url)
        {
            var json = FetchJson(url);
            var rootObject = JsonSerializer.Deserialize<JsonElement>(json);

            var vendorList = new VendorList();

            vendorList.GvlSpecificationVersion = ReadJsonNumber(rootObject, "gvlSpecificationVersion");
            vendorList.VendorListVersion = ReadJsonNumber(rootObject, "vendorListVersion");
            vendorList.TcfPolicyVersion = ReadJsonNumber(rootObject, "tcfPolicyVersion");
            vendorList.LastUpdated = DateTime.Parse(ReadJsonString(rootObject, "lastUpdated"));
            vendorList.Purposes = ReadPurposeCollection(ReadJsonObject(rootObject, "purposes"));
            vendorList.SpecialPurposes = ReadPurposeCollection(ReadJsonObject(rootObject, "specialPurposes"));
            vendorList.Features = ReadFeatureCollection(ReadJsonObject(rootObject, "features"));
            vendorList.SpecialFeatures = ReadFeatureCollection(ReadJsonObject(rootObject, "specialFeatures"));

            vendorList.Stacks = ReadStackCollection(ReadJsonObject(rootObject, "stacks"), new StackLookup
            {
                Purposes = vendorList.Purposes,
                SpecialFeatures = vendorList.SpecialFeatures,
            });
            vendorList.Vendors = ReadVendorCollection(ReadJsonObject(rootObject, "vendors"), new VendorLookup
            {
                Purposes = vendorList.Purposes,
                SpecialPurposes = vendorList.SpecialPurposes,
                Features = vendorList.Features,
                SpecialFeatures = vendorList.SpecialFeatures,
            });

            return vendorList;
        }

        protected string FetchJson(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }

        protected PurposeCollection ReadPurposeCollection(ObjectEnumerator purposes)
        {
            var collection = new PurposeCollection();

            foreach (var item in purposes)
            {
                collection.Add(ReadPurpose(item.Value));
            }

            return collection;
        }

        protected PurposeCollection ReadPurposeCollection(int[] purposeIds, PurposeCollection lookup)
        {
            var collection = new PurposeCollection(purposeIds.Length);

            foreach (var purposeId in purposeIds)
            {
                if (lookup.TryGet(purposeId, out var purpose))
                {
                    collection.Add(purpose);
                }
            }

            return collection;
        }

        protected Purpose ReadPurpose(JsonElement element)
        {
            var purpose = new Purpose();

            purpose.Id = ReadJsonNumber(element, "id");
            purpose.Name = ReadJsonString(element, "name");
            purpose.Description = ReadJsonString(element, "description");
            purpose.DescriptionLegal = ReadJsonString(element, "descriptionLegal");

            purpose.Consentable = ReadJsonBoolean(element, "consentable", true);
            purpose.RightToObject = ReadJsonBoolean(element, "rightToObject", true);

            return purpose;
        }

        protected FeatureCollection ReadFeatureCollection(ObjectEnumerator features)
        {
            var collection = new FeatureCollection();

            foreach (var item in features)
            {
                collection.Add(ReadFeature(item.Value));
            }

            return collection;
        }

        protected FeatureCollection ReadFeatureCollection(int[] featureIds, FeatureCollection lookup)
        {
            var collection = new FeatureCollection(featureIds.Length);

            foreach (var featureId in featureIds)
            {
                if (lookup.TryGet(featureId, out var purpose))
                {
                    collection.Add(purpose);
                }
            }

            return collection;
        }

        protected Feature ReadFeature(JsonElement element)
        {
            var feature = new Feature();

            feature.Id = ReadJsonNumber(element, "id");
            feature.Name = ReadJsonString(element, "name");
            feature.Description = ReadJsonString(element, "description");
            feature.DescriptionLegal = ReadJsonString(element, "descriptionLegal");

            return feature;
        }

        protected StackCollection ReadStackCollection(ObjectEnumerator stacks, StackLookup lookup)
        {
            var collection = new StackCollection();

            foreach (var item in stacks)
            {
                collection.Add(ReadStack(item.Value, lookup));
            }

            return collection;
        }

        protected Stack ReadStack(JsonElement element, StackLookup lookup)
        {
            var stack = new Stack();

            stack.Id = ReadJsonNumber(element, "id");
            stack.Name = ReadJsonString(element, "name");
            stack.Description = ReadJsonString(element, "description");

            stack.Purposes = ReadPurposeCollection(ReadIdArray(element, "purposes"), lookup.Purposes);
            stack.SpecialFeatures = ReadFeatureCollection(ReadIdArray(element, "specialFeatures"), lookup.SpecialFeatures);

            return stack;
        }

        protected VendorCollection ReadVendorCollection(ObjectEnumerator vendors, VendorLookup lookup)
        {
            var collection = new VendorCollection();

            foreach (var item in vendors)
            {
                collection.Add(ReadVendor(item.Value, lookup));
            }

            return collection;
        }

        protected Vendor ReadVendor(JsonElement element, VendorLookup lookup)
        {
            var vendor = new Vendor();

            vendor.Id = ReadJsonNumber(element, "id");
            vendor.Name = ReadJsonString(element, "name");
            vendor.PolicyUrl = ReadJsonString(element, "policyUrl");

            vendor.Purposes = ReadPurposeCollection(ReadIdArray(element, "purposes"), lookup.Purposes);
            vendor.SpecialPurposes = ReadPurposeCollection(ReadIdArray(element, "specialPurposes"), lookup.SpecialPurposes);
            vendor.LegitimateInterestPurposes = ReadPurposeCollection(ReadIdArray(element, "legIntPurposes"), lookup.Purposes);
            vendor.FlexiblePurposes = ReadPurposeCollection(ReadIdArray(element, "flexiblePurposes"), lookup.Purposes);

            vendor.Features = ReadFeatureCollection(ReadIdArray(element, "features"), lookup.Features);
            vendor.SpecialFeatures = ReadFeatureCollection(ReadIdArray(element, "specialFeatures"), lookup.SpecialFeatures);

            if (element.TryGetProperty("deletedDate", out var deletedDateElement))
            {
                vendor.DeletedDate = DateTime.Parse(deletedDateElement.GetString());
            }

            if (element.TryGetProperty("overflow", out var overflowElement))
            {
                vendor.Overflow = new Vendor.OverflowOptions
                {
                    HttpGetLimit = ReadJsonNumber(overflowElement, "httpGetLimit"),
                };
            }

            return vendor;
        }

        protected bool ReadJsonBoolean(JsonElement element, string propertyName, bool defaultValue = default)
        {
            if (element.TryGetProperty(propertyName, out var value))
            {
                return value.GetBoolean();
            }
            else
            {
                return defaultValue;
            }
        }

        protected int ReadJsonNumber(JsonElement element, string propertyName, int defaultValue = default)
        {
            if (element.TryGetProperty(propertyName, out var value))
            {
                return value.GetInt32();
            }
            else
            {
                return defaultValue;
            }
        }

        protected string ReadJsonString(JsonElement element, string propertyName, string defaultValue = default)
        {
            if (element.TryGetProperty(propertyName, out var value))
            {
                return value.GetString();
            }
            else
            {
                return defaultValue;
            }
        }

        protected ArrayEnumerator ReadJsonArray(JsonElement element, string propertyName)
        {
            return element.GetProperty(propertyName).EnumerateArray();
        }

        protected int[] ReadIdArray(JsonElement element, string propertyName)
        {
            return ReadJsonArray(element, propertyName)
                .Select(x => x.GetInt32())
                .ToArray();
        }

        protected ObjectEnumerator ReadJsonObject(JsonElement element, string propertyName)
        {
            return element.GetProperty(propertyName).EnumerateObject();
        }

        protected struct StackLookup
        {
            public PurposeCollection Purposes { get; set; }
            public FeatureCollection SpecialFeatures { get; set; }
        }

        protected struct VendorLookup
        {
            public PurposeCollection Purposes { get; set; }
            public PurposeCollection SpecialPurposes { get; set; }
            public FeatureCollection Features { get; set; }
            public FeatureCollection SpecialFeatures { get; set; }
        }
    }
}
