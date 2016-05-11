using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace HelperPack.Tests
{
    public class StringExtensionsTests
    {
        [Fact]
        public void StringExtensions_StripTags()
        {
            string sample = "abc<tag>cde";
            string expected = "abccde";

            StringExtensions.StripTags(sample).Should().Be(expected);
        }

        [Fact]
        public void StringExtensions_StripCommentTags()
        {
            string sample = "abc<!--tag-->cde";
            string expected = "abccde";

            StringExtensions.StripTags(sample).Should().Be(expected);
        }

        [Fact]
        public void StringExtensions_ReplaceTags()
        {
            string sample = "abc<tag>cde";
            string expected = "abc     cde";

            StringExtensions.StripTags(sample, true).Should().Be(expected);
        }

        [Fact]
        public void StringExtensions_ReplaceEmptyTags()
        {
            string sample = "abc<>cde";
            string expected = "abc  cde";
            StringExtensions.StripTags(sample, true).Should().Be(expected);
        }

        [Fact]
        public void StringExtensions_GzipCompressionDecompressTest()
        {
            string input = "abcdefghijklmn0123456789";
            string expected = "abcdefghijklmn0123456789";
            string compressedText = StringExtensions.GzipCompress(input);
            string actual = StringExtensions.GzipDecompress(compressedText);

            actual.Should().Be(expected);
        }

        [Fact]
        public void StringExtensions_CaseInsensitiveEquals()
        {
            string left = "abcdefghijklmn0123456789";
            string right = "abcdefghijklmn0123456789";

            StringExtensions.CaseInsensitiveEquals(left, right).Should().BeTrue();
            StringExtensions.CaseSensitiveEquals(left, right).Should().BeTrue();

            StringExtensions.CaseInsensitiveEndsWith(left, right).Should().BeTrue();

            StringExtensions.CaseInsensitiveEquals(string.Empty, string.Empty).Should().BeTrue();
            StringExtensions.CaseSensitiveEquals(string.Empty, string.Empty).Should().BeTrue();


            StringExtensions.CaseInsensitiveEquals(null, null).Should().BeFalse();
            StringExtensions.CaseSensitiveEquals(null, null).Should().BeFalse();

            StringExtensions.CaseInsensitiveEquals(null, string.Empty).Should().BeFalse();
            StringExtensions.CaseSensitiveEquals(null, string.Empty).Should().BeFalse();


            StringExtensions.CaseInsensitiveEquals("A", "a").Should().BeTrue();
            StringExtensions.CaseSensitiveEquals("A", "a").Should().BeFalse();


            StringExtensions.CaseInsensitiveStartsWith("A", "a").Should().BeTrue();
            StringExtensions.CaseSensitiveStartsWith("A", "a").Should().BeFalse();

            StringExtensions.CaseInsensitiveEndsWith("A", "a").Should().BeTrue();
            StringExtensions.CaseSensitiveEndsWith("A", "a").Should().BeFalse();

            StringExtensions.CaseInsensitiveContains("A", "a").Should().BeTrue();
            StringExtensions.CaseSensitiveContains("A", "a").Should().BeFalse();
        }

        [Fact]
        public void StringExtensions_StripTags_Test()
        {
            string test1 = "abc";
            string test2 = string.Empty;
            string test3 = null;
            string test4 = "<a>abc";
            string test4out = "abc";
            string test5 = "</a>abc";
            string test5out = "abc";
            string test6 = "<abc>";
            string test6out = string.Empty;

            StringExtensions.StripTags(test1).Should().Be(test1);
            StringExtensions.StripTags(test2).Should().Be(test2);
            StringExtensions.StripTags(test3).Should().Be(test3);
            StringExtensions.StripTags(test4).Should().Be(test4out);
            StringExtensions.StripTags(test5).Should().Be(test5out);
            StringExtensions.StripTags(test6).Should().Be(test6out);
        }

        [Fact]
        public void StringExtensions_JoinStringCollection_Test()
        {
            IEnumerable<string> input = null;
            StringExtensions.Join(input, ",").Should().BeEmpty();

            input = new List<string>() { "a", "b", "c" };
            StringExtensions.Join(input, ",").Should().Be("a,b,c");
        }

        enum TestEnum { A, B, C };

        [Fact]
        public void StringExtensions_ToEnumTest()
        {
            StringExtensions.ToEnum<TestEnum>("A").Should().Be(TestEnum.A);
            StringExtensions.ToEnum<TestEnum>("B").Should().Be(TestEnum.B);
            StringExtensions.ToEnum<TestEnum>("C").Should().Be(TestEnum.C);
        }

        [Fact]
        public void StringExtensions_ToEnumThrowOnBadValueTest()
        {
            Assert.Throws<ArgumentException>(() => StringExtensions.ToEnum<TestEnum>("D"));
        }

        [Fact]
        public void StringExtensions_ToEnumThrowsOnBadTypeTest()
        {
            Assert.Throws<InvalidOperationException>(() => StringExtensions.ToEnum<int>("A"));
        }

        [Fact]
        public void StringExtensions_Left()
        {
            StringExtensions.Left("asd", 3).Should().Be("asd");
            StringExtensions.Left(string.Empty, 3).Should().BeEmpty();

            StringExtensions.Left(((string)null), 4).Should().BeNull();

            StringExtensions.Left("asdfghj", 3).Should().Be("asd");
            StringExtensions.Left("asdfghj", 0).Should().BeEmpty();
        }

        [Fact]
        public void StringExtensions_Left_ThrowsOnNegativeSize()
        {
            Assert.Throws<ArgumentException>(() => StringExtensions.Left("asd", -1));
        }

        [Fact]
        public void StringExtensions_Right()
        {
            StringExtensions.Right("asd", 3).Should().Be("asd");
            StringExtensions.Right(string.Empty, 3).Should().BeEmpty();

            StringExtensions.Right(((string)null), 4).Should().BeNull();

            StringExtensions.Right("asdfghj", 3).Should().Be("ghj");
            StringExtensions.Right("asdfghj", 0).Should().BeEmpty();
        }

        [Fact]
        public void StringExtensions_Rght_ThrowsOnNegativeSize()
        {
            Assert.Throws<ArgumentException>(() => StringExtensions.Right("asd", -1));
        }

        [Fact]
        public void StringExtensions_Collection_Contains()
        {
            string[] source = { "abc", "ABC", "cde", null };

            StringExtensions.CaseInsensitiveContains(source, "abc").Should().BeTrue();
            StringExtensions.CaseInsensitiveContains(source, "ABC").Should().BeTrue();
            StringExtensions.CaseInsensitiveContains(source, "cde").Should().BeTrue();
            StringExtensions.CaseInsensitiveContains(source, "CDE").Should().BeTrue();

            StringExtensions.CaseInsensitiveContains(source, "ab").Should().BeFalse();
            StringExtensions.CaseInsensitiveContains(source, "cd").Should().BeFalse();

            StringExtensions.CaseSensitiveContains(source, "AB").Should().BeFalse();
            StringExtensions.CaseSensitiveContains(source, "cd").Should().BeFalse();

            StringExtensions.CaseSensitiveContains(source, "ABC").Should().BeTrue();
            StringExtensions.CaseSensitiveContains(source, "cde").Should().BeTrue();
            StringExtensions.CaseSensitiveContains(source, "CDE").Should().BeFalse();
        }

        [Fact]
        public void StringExtensions_RightAtWord_NegativeLength()
        {
            Assert.Throws<ArgumentException>(() => StringExtensions.RightAtWord(string.Empty, -1));
        }

        [Fact]
        public void StringExtensions_RightAtWord()
        {
            string input = "aaa asd";

            StringExtensions.RightAtWord(string.Empty, 0).Should().BeEmpty();
            StringExtensions.RightAtWord(((string)null), 10).Should().BeEmpty();

            StringExtensions.RightAtWord(input, 0).Should().BeEmpty();
            StringExtensions.RightAtWord(input, 1).Should().BeEmpty();
            StringExtensions.RightAtWord(input, 2).Should().BeEmpty();
            StringExtensions.RightAtWord(input, 3).Should().Be("asd");
            StringExtensions.RightAtWord(input, 4).Should().Be("asd");
            StringExtensions.RightAtWord(input, 5).Should().Be("asd");
            StringExtensions.RightAtWord(input, 6).Should().Be("asd");
            StringExtensions.RightAtWord(input, 7).Should().Be("aaa asd");
            StringExtensions.RightAtWord(input, 8).Should().Be("aaa asd");
        }

        [Fact]
        public void StringExtensions_LeftAtWord_NegativeLength()
        {
            Assert.Throws<ArgumentException>(() => StringExtensions.LeftAtWord(string.Empty, -1));
        }

        [Fact]
        public void StringExtensions_LeftAtWord()
        {
            string input = "asd aaa";

            StringExtensions.LeftAtWord(input, 0).Should().BeEmpty();
            StringExtensions.LeftAtWord(input, 1).Should().BeEmpty();
            StringExtensions.LeftAtWord(input, 2).Should().BeEmpty();
            StringExtensions.LeftAtWord(input, 3).Should().Be("asd");
            StringExtensions.LeftAtWord(input, 4).Should().Be("asd");
            StringExtensions.LeftAtWord(input, 5).Should().Be("asd");
            StringExtensions.LeftAtWord(input, 6).Should().Be("asd");
            StringExtensions.LeftAtWord(input, 7).Should().Be("asd aaa");
            StringExtensions.LeftAtWord(input, 8).Should().Be("asd aaa");
        }

        [Fact]
        public void StringExtensions_LastWords_NegativeCount()
        {
            Assert.Throws<ArgumentException>(() => StringExtensions.LastWords(string.Empty, -1));
        }

        [Fact]
        public void StringExtensions_LastWords()
        {
            string input = "abc cde fgh";

            StringExtensions.LastWords(input, 0).Should().BeEmpty();
            StringExtensions.LastWords(input, 1).Should().Be("fgh");
            StringExtensions.LastWords(input, 2).Should().Be("cde fgh");
            StringExtensions.LastWords(input, 3).Should().Be(input);
            StringExtensions.LastWords(input, 4).Should().Be(input);
        }

        [Fact]
        public void StringExtensions_FirstWords_NegativeCount()
        {
            Assert.Throws<ArgumentException>(() => StringExtensions.FirstWords(string.Empty, -1));
        }

        [Fact]
        public void StringExtensions_FirstWords()
        {
            string input = "abc cde fgh";

            StringExtensions.FirstWords(input, 0).Should().BeEmpty();
            StringExtensions.FirstWords(input, 1).Should().Be("abc");
            StringExtensions.FirstWords(input, 2).Should().Be("abc cde");
            StringExtensions.FirstWords(input, 3).Should().Be(input);
            StringExtensions.FirstWords(input, 4).Should().Be(input);
        }

        [Fact]
        public void StringExtensions_PreviousLines_NegativePosition()
        {
            Assert.Throws<ArgumentException>(() => StringExtensions.PreviousLines(string.Empty, -1, 0));
        }

        [Fact]
        public void StringExtensions_PreviousLines_NegativeCount()
        {
            Assert.Throws<ArgumentException>(() => StringExtensions.PreviousLines(string.Empty, 0, -1));
        }

        [Fact]
        public void StringExtensions_PreviousLines_StartAtZero()
        {
            string test = "sample";
            string result = StringExtensions.PreviousLines(test, 0, 10);
            result.Should().BeEmpty();
        }

        [Fact]
        public void StringExtensions_PreviousLines_StartAtOne()
        {
            string test = "sample";
            string result = StringExtensions.PreviousLines(test, 1, 10);
            string expected = "s";
            result.Should().Be(expected);
        }

        [Fact]
        public void StringExtensions_PrevousLines()
        {
            string input = "l1\nl2\nl3\nl4\nl5";
            int idx = input.IndexOf("l3") + 1;

            StringExtensions.PreviousLines(((string)null), 0, 0).Should().BeEmpty();

            StringExtensions.PreviousLines(input, 0, 0).Should().BeEmpty();

            StringExtensions.PreviousLines(input, idx, 0).Should().Be("l3");
            StringExtensions.PreviousLines(input, idx, 1).Should().Be("l2\nl3");
            StringExtensions.PreviousLines(input, idx, 2).Should().Be("l1\nl2\nl3");
            StringExtensions.PreviousLines(input, idx, 3).Should().Be("l1\nl2\nl3");
        }

        [Fact]
        public void StringExtensions_NextLines_NegativePosition()
        {
            Assert.Throws<ArgumentException>(() => StringExtensions.NextLines(string.Empty, -1, 0));
        }

        [Fact]
        public void StringExtensions_NextLines_NegativeCount()
        {
            Assert.Throws<ArgumentException>(() => StringExtensions.NextLines(string.Empty, 0, -1));
        }

        [Fact]
        public void StringExtensions_NextLines_Test()
        {
            string input = "aaa\nbbb";
            StringExtensions.NextLines(input, 5, 10).Should().Be("bb");
        }

        [Fact]
        public void StringExtensions_NextLines()
        {
            string input = "l1\nl2\nl3\nl4\nl5";
            int idx = input.IndexOf("l3") + 1;

            StringExtensions.NextLines(((string)null), 0, 0).Should().BeEmpty();

            StringExtensions.NextLines(input, 0, 0).Should().Be("l1");

            StringExtensions.NextLines(input, idx, 0).Should().Be("3");
            StringExtensions.NextLines(input, idx, 1).Should().Be("3\nl4");
            StringExtensions.NextLines(input, idx, 2).Should().Be("3\nl4\nl5");
            StringExtensions.NextLines(input, idx, 3).Should().Be("3\nl4\nl5");
        }

        [Fact]
        public void StringExtensions_FirstLines_NegativeCount()
        {
            Assert.Throws<ArgumentException>(() => StringExtensions.FirstLines(string.Empty, -1));
        }

        [Fact]
        public void StringExtensions_FirstLines()
        {
            string input = "l1\nl2\nl3\nl4\nl5";

            StringExtensions.FirstLines(((string)null), 0).Should().BeEmpty();

            StringExtensions.FirstLines(input, 0).Should().BeEmpty();
            StringExtensions.FirstLines(input, 1).Should().Be("l1");
            StringExtensions.FirstLines(input, 2).Should().Be("l1\nl2");
            StringExtensions.FirstLines(input, 3).Should().Be("l1\nl2\nl3");
            StringExtensions.FirstLines(input, 4).Should().Be("l1\nl2\nl3\nl4");
            StringExtensions.FirstLines(input, 5).Should().Be("l1\nl2\nl3\nl4\nl5");
            StringExtensions.FirstLines(input, 6).Should().Be("l1\nl2\nl3\nl4\nl5");
        }

        [Fact]
        public void StringExtensions_LastLines_NegativeCount()
        {
            Assert.Throws<ArgumentException>(() => StringExtensions.LastLines(string.Empty, -1));
        }

        [Fact]
        public void StringExtensions_LastLines()
        {
            string input = "l1\nl2\nl3\nl4\nl5";

            StringExtensions.LastLines(((string)null), 0).Should().BeEmpty();

            StringExtensions.LastLines(input, 0).Should().BeEmpty();
            StringExtensions.LastLines(input, 1).Should().Be("l5");
            StringExtensions.LastLines(input, 2).Should().Be("l4\nl5");
            StringExtensions.LastLines(input, 3).Should().Be("l3\nl4\nl5");
            StringExtensions.LastLines(input, 4).Should().Be("l2\nl3\nl4\nl5");
            StringExtensions.LastLines(input, 5).Should().Be("l1\nl2\nl3\nl4\nl5");
            StringExtensions.LastLines(input, 6).Should().Be("l1\nl2\nl3\nl4\nl5");
        }

        [Fact]
        public void StringExtensions_AllIndexesOf()
        {
            string input = "asd dsa asd";
            IEnumerable<int> expected = new int[] { 0, 8 };
            StringExtensions.AllIndexesOf(input, "asd").Should().Equal(expected);
        }

        [Fact]
        public void StringExtensions_AllIndexesOf_tags()
        {
            string input = "asd dsa asd";
            IEnumerable<string> tags = new string[] { "asd", "dsa" };

            var expected = new[] 
            {
                Tuple.Create(0,"asd"),
                Tuple.Create(4,"dsa"),
                Tuple.Create(8,"asd")
            };

            StringExtensions.AllIndexesOf(input, tags).Should().Equal(expected);
        }

        [Fact]
        public void StringExtensions_Highlight_tags()
        {
            string input = "asd dsa asd";
            IEnumerable<string> tags = new string[] { "asd", "dsa" };

            string expected = "*asd* *dsa* *asd*";

            StringExtensions.Highlight(input, "*{0}*", tags).Should().Be(expected);
        }

        [Fact]
        public void StringExtensions_Join_EmptyCollection()
        {
            string[] input = { };
            string empty = "empty";

            StringExtensions.Join(input, ",", empty).Should().Be(empty);
        }

        [Fact]
        public void StringExtentions_StripHtml()
        {
            string input = "<test/>a<test>a</test> &nbsp;&amp;&gt;&lt;&quot;&hellip;";
            string strip = StringExtensions.StripHTML(input);
            string expected = "aa  &><\"…";
            strip.Should().Be(expected);
        }
    }
}
