# Transparency and Consent Framework C# SDK

A library written in C# which provides functionality to parse and serialize IAB TCF Strings.

This library currently only supports v2.0 of the specification.

[![Tests](https://github.com/bidtellect/tcf/actions/workflows/tests.yml/badge.svg)](https://github.com/bidtellect/tcf/actions/workflows/tests.yml)

# Usage

## Fetch the Global Vendor List (GVL)

You can get the most recent version of the GVL using the `GvlClient`:

```cs
var client = new GvlClient();

VendorList gvl = client.Fetch();
```

Note: The GVL is not needed to parse TC strings as long as you know the Purpose and Vendor IDs.

## Parse a TC String

You can parse a TC String using the `TcStringParser`:

```cs
var tcString = "COvFyGBOvFyGBAbAAAENAPCAAOAAAAAAAAAAAEEUACCKAAA.IFoEUQQgAIQwgIwQABAEAAAAOIAACAIAAAAQAIAgEAACEAAAAAgAQBAAAAAAAGBAAgAAAAAAAFAAECAAAgAAQARAEQAAAAAJAAIAAgAAAYQEAAAQmAgBC3ZAYzUw";

// GVL can be null if the metadata is not needed.
var parser = new TcStringParser(gvl);

TcString model = parser.Parse(tcString);
```

You can instruct the parser to skip certain segments of the TC Strings using `ParseOptions`:

```cs
TcString model = parser.Parse(tcString, new TcStringParser.ParseOptions {
    ExcludePublisherTc = true,
});
```

## Serialize a TC String

You can serialize a `TcString` object back into a `string` representation using `TcStringSerializer`:

```cs
var serializer = new TcStringSerializer();

string message = serializer.Serialize(tcString);
```
