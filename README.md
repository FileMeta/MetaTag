# MetaTag
A metatag is like a hashtag in that it can be embedded wherever text is stored. However, where a hashtag is only a label or keyword, a metatag has a name and a value. Thus, metatags allow custom metadata to be stored in existing text or textual fields such as comments.

Metatag Examples:

* &author=Brandt
* &subject="MetaTag Format"
* &date=2018-12-17T21:22:05-06:00
* &references=https://en.wikipedia.org/wiki/Metadata

## Format Summary

Please see the [MetaTag Specification](https://www.filemeta.org/MetaTag) for a formal definition of the format.

A metatag starts with an ampersand - just as a hashtag starts with the hash symbol.

Next comes the name which follows the same standard as a hashtag - it must be composed of letters, numbers, and the underscore character. Rigorous implementations should use the unicode character sets. Specifically Unicode categories: Ll, Lu, Lt, Lo, Lm, Mn, Nd, Pc. For regular expressions this matches the \w chacter class.

Next is an equals sign.

Next is the value which may be in plain or quoted form. In plain form, the value is a series of one or more non-whitespace and non-quote characters. The value is terminated by whitespace or the end of the document.

Quoted form is a quotation mark followed by zero or more non-quote characters and terminated with another quotation mark. Newlines and other whitespace are permitted within the quoted text. A pair of quotation marks in the text is interpreted as a singe quotation mark in the value and does not terminate the value.

## The MetaTag Class

The MetaTag class includes static methods for encoding and decoding metatags and for extracting metatags from text and for embedding metatags into text. All are documented using embedded [documentation comments](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/xml-documentation-comments).

## About MetaTag
The software is distributed in C# as a [CodeBit](http://filemeta.org/CodeBit.html) located [here](https://raw.githubusercontent.com/FileMeta/MetaTag/master/MetaTag.cs). It is released under a [BSD 3-Clause](https://opensource.org/licenses/BSD-3-Clause) open source license.

MetaTag is part of the [FileMeta](http://www.filemeta.org/) project.

This project includes the master copy of the [MetaTag.cs](https://raw.githubusercontent.com/FileMeta/MetaTag/master/MetaTag.cs) CodeBit plus a set of unit tests which may also serve as sample code.

## About CodeBits
A [CodeBit](https://www.FileMeta.org/CodeBit) is very lightweight way to share common code. Each CodeBit consists of a single source code file. A structured comment at the beginning of the file indicates where to find the master copy so that automated tools can retrieve and update CodeBits to the latest version.
