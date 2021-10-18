# Transparency and Consent Framework C# SDK

A library written in C# which provides functionality to parse and serialize IAB TCF Strings.

This library currently only supports v2.0 of the specification.

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

# Fibonacci Encoding

TC Strings with lots of Vendor IDs in it can be very long.
Fibonacci encoding is a variable-length encoding scheme that, when used to encode Vendor IDs,
should result in TC String that are substantially shorter.

In TCF v2.0, Vendor IDs are encoded in Base-2 using 16-bit words:


```text
[------------ 16 bits -----------]
0 0 0 0  0 0 0 0  0 0 0 0  0 0 0 0
│ │ │ │  │ │ │ │  │ │ │ │  │ │ │ │
│ │ │ │  │ │ │ │  │ │ │ │  │ │ │ └ 2^0  =      1
│ │ │ │  │ │ │ │  │ │ │ │  │ │ └── 2^1  =      2
│ │ │ │  │ │ │ │  │ │ │ │  │ └──── 2^2  =      4
│ │ │ │  │ │ │ │  │ │ │ │  └────── 2^3  =      8
│ │ │ │  │ │ │ │  │ │ │ │
│ │ │ │  │ │ │ │  │ │ │ └───────── 2^4  =     16
│ │ │ │  │ │ │ │  │ │ └─────────── 2^5  =     32
│ │ │ │  │ │ │ │  │ └───────────── 2^6  =     64
│ │ │ │  │ │ │ │  └─────────────── 2^7  =    128
│ │ │ │  │ │ │ │
│ │ │ │  │ │ │ └────────────────── 2^8  =    256
│ │ │ │  │ │ └──────────────────── 2^9  =    512
│ │ │ │  │ └────────────────────── 2^10 =  1,024
│ │ │ │  └──────────────────────── 2^11 =  2,048
│ │ │ │
│ │ │ └─────────────────────────── 2^12 =  4,096
│ │ └───────────────────────────── 2^13 =  8,192
│ └─────────────────────────────── 2^14 = 16,384
└───────────────────────────────── 2^15 = 32,768
```

All Vendor IDs take the same amount bits.
ID `12` takes the same amount of bits as ID `920`; 16 bits.
A variable-length encoding scheme allows for smaller numbers to be encoded using less bits than larger numbers.

A **Fibonacci Series** is any series of numbers where the next number is the sum of the previous two numbers.
Traditionally these numbers are `0` and `1`.
For Example:

```text
0, 1, 1, 2, 3, 5, 8, 13, ...
```


**Fibonacci Encoding** is based on _Zeckendorf's theorem_, which states:

> Every positive integer can be represented uniquely as the sum of one or more distinct Fibonacci numbers in such a way that the sum does not include any two consecutive Fibonacci numbers.

Knowing this, we can uniquely encode any positive integer greater than `0` using a bit field where each bit represents a number in a Fibonacci Series with the initial values of `0` and `1`.

```text
[------ n bits -----]
0 0 0 0  0 0 0 0  ...
│ │ │ │  │ │ │ │
│ │ │ │  │ │ │ └ f(9) = 34
│ │ │ │  │ │ └── f(8) = 21
│ │ │ │  │ └──── f(7) = 13
│ │ │ │  └────── f(6) =  8
│ │ │ │
│ │ │ └───────── f(5) =  5
│ │ └─────────── f(4) =  3
│ └───────────── f(3) =  2
└─────────────── f(2) =  1
```

Notice that we start at `f(2)`.

In this scheme, you would only naturally see the bit patterns `00`, `01`, and `10`.
That means that we can use `11` to indicate the end of the number we're trying to encode.

Here is an example of how that would work:


| Number | Binary Representation | Bits Used |
|--------|-----------------------|-----------|
|      1 | `11`                  |         2 |
|      2 | `011`                 |         3 |
|      3 | `0011`                |         4 |
|      4 | `1011`                |         4 |
|      5 | `00011`               |         5 |
|      6 | `10011`               |         5 |
|      7 | `01011`               |         5 |
|      8 | `000011`              |         6 |
|      9 | `100011`              |         6 |
|     10 | `010011`              |         6 |
|     11 | `001011`              |         6 |
|     12 | `101011`              |         6 |
|     13 | `0000011`             |         7 |

Notice that small numbers take very few bits to encode.
But the number of bits required to encoded a number increases as the number increases.
In fact, the rate at which they increase follows the Fibonacci Series.
By the time we get to the number `987` we're already using 16 bits; which means we're not saving any space over base-2 encoding.

| Number | Binary Representation | Bits Used |
|--------|-----------------------|-----------|
|    986 | `010101010101011`     |        15 |
|    987 | `0000000000000011`    |        16 |
|    988 | `1000000000000011`    |        16 |

The best way to take advantage of Fibonacci encoding is to use it to represent small numbers.
We can do this by writing the Vendor ranges as offsets from the previous range. Offsets will generally be small numbers where Vendor IDs will only increase in value.

## Implementation

Fibonacci encoding is implemented by extending the `TcStringParser` and `TcStringSerializer` classes and overriding some of the methods.

The new classes used to encode and decode TC Strings using Fib Encoding are:

- `TcStringParserFib`: Used to parse TC Strings encoded using Fib Encoding.
- `TcStringSerializerFib`: Used to Serialize TC Strings using Fib Encoding.
