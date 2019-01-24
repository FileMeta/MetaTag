# MetaTag
A metatag is like a hashtag in that it can be embedded wherever text is stored. However, where a hashtag is only a label or keyword, a metatag has a name and a value. Thus, metatags allow custom metadata to be stored in existing text or textual fields such as comments.

Metatag Examples:

* &author=Brandt
* &subject=MetaTag_Format
* &date=2018-12-17T21:22:05-06:00

## Format Definition

A metatag starts with an ampersand - just as a hashtag starts with the hash symbol.

Next comes the name which follows the same standard as a hashtag - it must be composed of letters, numbers, and the underscore character. Rigorous implementations should use the unicode character sets. Specifically Unicode categories: Ll, Lu, Lt, Lo, Lm, Mn, Nd, Pc. For regular expressions this matches the \w chacter class.

Next is an equals sign.

Next is the value which is a series of any characters except the ASCII control range (0x00 to 0x7F), the space or the ampersand. Control characters, space, ampersand, underscore, and the percent character MUST be encoded. A space character is encoded as the underscore. All other control, ampersand, underscore, or percent characters are encoded as the percent character followed by two hexadecimal digits. All characters requiring encoding are in the first 256 characters of Unicode, so two hexadecimal digits are sufficient. Other Unicode characters are given by their literal value.

The value encoding is deliberately similar to URL query string encoding. However, in Metatag encoding, the underscore substitutes for a space whereas in URL query strings, the plus sign substitutes for a space.

The name IS NOT encoded. Valid names are simply limited to the specified character set.

## The MetaTag Class

The MetaTag class includes static methods for encoding and decoding metatags and for extracting metatags from text and for embedding metatags into text. All are documented using embedded [documentation comments](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/xml-documentation-comments).

## About MetaTag
The software is distributed in C# as a [CodeBit](http://filemeta.org/CodeBit.html) located [here](https://raw.githubusercontent.com/FileMeta/MetaTag/master/MetaTag.cs). It is released under a [BSD 3-Clause](https://opensource.org/licenses/BSD-3-Clause) open source license.

MetaTag is part of the [FileMeta](http://www.filemeta.org/) initiative.

This project includes the master copy of the [MetaTag.cs](https://raw.githubusercontent.com/FileMeta/MetaTag/master/MetaTag.cs) CodeBit plus a set of unit tests which may also serve as sample code.

## About CodeBits
A [CodeBit](https://www.FileMeta.org/CodeBit.html) is very lightweight way to share common code. Each CodeBit consists of a single source code file. A structured comment at the beginning of the file indicates where to find the master copy so that automated tools can retrieve and update CodeBits to the latest version.
