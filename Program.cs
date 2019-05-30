using System;
using System.Collections.Generic;

namespace FileMeta
{

    /// <summary>
    /// Unit tests for the MetaTag and MetaTagSet classes.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Testing EncodeValue.");
                TestEncodeDecode(string.Empty, string.Empty);
                TestEncodeDecode("Simple", "Simple");
                TestEncodeDecode("Sentence with spaces.", "\"Sentence with spaces.\"");
                TestEncodeDecode("\a\b\t\r\v\f\n\x1b", "\"\a\b\t\r\v\f\n\x1b\"");
                TestEncodeDecode("Underscore_Percent%And-Ampersand&Not-Encoded.", "Underscore_Percent%And-Ampersand&Not-Encoded.");
                TestEncodeDecode("Quotes\"Doubled", "\"Quotes\"\"Doubled\"");
                TestEncodeDecode("Unencoded-Punctuation:!#$'()*+,-./:;<=>?@[\\]^`{|}~", "Unencoded-Punctuation:!#$'()*+,-./:;<=>?@[\\]^`{|}~");
                TestEncodeDecode(" Leading Space", "\" Leading Space\"");
                TestEncodeDecode("\x00\x01\x02", "\x00\x01\x02"); // Embedded null

                Console.WriteLine();
                Console.WriteLine("Testing DecodeValue.");
                TestDecodeValue("\"\"\"This is a test.\"\"\"", "\"This is a test.\"");

                Console.WriteLine();
                Console.WriteLine("Testing Parse and Format");
                TestParseAndFormat("&name=value", "name", "value");
                TestParseAndFormat("&complex_name=\"Encoded Value \"\"This\"\" is fun\"", "complex_name", "Encoded Value \"This\" is fun");

                Console.WriteLine();
                Console.WriteLine("Testing embedded extraction and update.");
                TestEmbedded();

                Console.WriteLine();
                Console.WriteLine("All tests succeeded.");
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
                Console.WriteLine();
            }

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
#endif
        }

        static void TestEncodeDecode(string value, string encoded)
        {
            Console.WriteLine(encoded);
            if (!string.Equals(MetaTag.EncodeValue(value), encoded, StringComparison.Ordinal))
            {
                throw new ApplicationException("Failed EncodeValue (EncodeDecode)");
            }
            if (!string.Equals(MetaTag.DecodeValue(encoded), value, StringComparison.Ordinal))
            {
                throw new ApplicationException("Failed DecodeValue (EncodeDecode)");
            }
        }

        static void TestDecodeValue(string encoded, string value)
        {
            Console.WriteLine(encoded);
            if (!string.Equals(MetaTag.DecodeValue(encoded), value, StringComparison.Ordinal))
            {
                throw new ApplicationException("Failed DecodeValue");
            }
            if (!string.Equals(MetaTag.DecodeValue(MetaTag.EncodeValue(value)), value, StringComparison.Ordinal))
            {
                throw new ApplicationException("Failed EncodeValue (TestDecodeValue)");
            }
        }

        static void TestParseAndFormat(string metaTag, string name, string value)
        {
            Console.WriteLine(metaTag);
            var val = MetaTag.Parse(metaTag);
            if (!string.Equals(val.Key, name, StringComparison.Ordinal))
            {
                throw new ApplicationException("Failed Parse (name)");
            }
            if (!string.Equals(val.Value, value, StringComparison.Ordinal))
            {
                throw new ApplicationException("Failed Parse (value)");
            }
            if (!string.Equals(MetaTag.Format(val), metaTag, StringComparison.Ordinal))
            {
                throw new ApplicationException("Failed Format (KeyValuePair)");
            }
            if (!string.Equals(MetaTag.Format(val.Key, val.Value), metaTag, StringComparison.Ordinal))
            {
                throw new ApplicationException("Failed Forat (name, value)");
            }
        }

        static void TestEmbedded()
        {
            var tagSet = new MetaTagSet();
            tagSet.Load(c_embedded1);

            string n = tagSet.EmbedAndUpdate(null);
            if (!string.Equals(n, c_normalized1, StringComparison.Ordinal))
            {
                throw new ApplicationException("Failed Embedded Extraction");
            }

            string u = MetaTag.EmbedAndUpdate(c_embedded1, s_update1);
            if (!string.Equals(u, c_embedded2))
            {
                throw new ApplicationException("Failed Embedded Update");
            }

            tagSet = new MetaTagSet();
            tagSet.Load(u);
            n = tagSet.EmbedAndUpdate(null);
            if (!string.Equals(n, c_normalized2))
            {
                throw new ApplicationException("Failed Embedded Update and Normalize");
            }
        }


        const string c_embedded1 =
@"This string &title=""Test """"MetaTag"""" Embedding"" contains embedded
MetaTags. &subject=""Unit Test"" It is being used to test the extraction
and &date=2018-01-23T19:03:22 embedding of metatags in a
&keywords=one;two;three continuous string of text.";

        const string c_normalized1 =
@"&date=2018-01-23T19:03:22 &keywords=one;two;three &subject=""Unit Test"" &title=""Test """"MetaTag"""" Embedding""";

        static readonly KeyValuePair<string, string>[] s_update1 = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("title", "Test \"MetaTag\" Updated"),
            new KeyValuePair<string, string>("subject", null), // Should remove the text
            new KeyValuePair<string, string>("keywords", "a;b;c"),
            new KeyValuePair<string, string>("expires", "2020-01-01"),
            new KeyValuePair<string, string>("author", "George Orwell"),
            new KeyValuePair<string, string>("publisher", "LightWave")
        };

        const string c_embedded2 =
@"This string &title=""Test """"MetaTag"""" Updated"" contains embedded
MetaTags. It is being used to test the extraction
and &date=2018-01-23T19:03:22 embedding of metatags in a
&keywords=a;b;c continuous string of text. &author=""George Orwell"" &expires=2020-01-01 &publisher=LightWave";

        const string c_normalized2 =
@"&author=""George Orwell"" &date=2018-01-23T19:03:22 &expires=2020-01-01 &keywords=a;b;c &publisher=LightWave &title=""Test """"MetaTag"""" Updated""";

    }
}
