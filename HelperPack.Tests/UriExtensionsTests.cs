using System;
using System.Linq;
using System.Net;
using FluentAssertions;
using Xunit;

namespace HelperPack.Tests
{

    public class UriExtensionsTests
    {
        [Fact]
        public void UriExtensions_ExtractUris_SimpleUri()
        {
            string input = "http://www.google.com";
            Uri expected = new Uri(input);

            UriExtensions.ExtractUris(input).Single().Should().Be(expected);
        }

        [Fact]
        public void UriExtensions_ExtractUris_FromHtml()
        {
            string input = "<a href=\"http://www.google.com\"><other";
            Uri expected = new Uri("http://www.google.com");

            UriExtensions.ExtractUris(input).Single().Should().Be(expected);
        }

        [Fact]
        public void UriExtensions_ExtractUris()
        {
            string input = " Support the awesome drummer Eddie Fisher of OneRepublic! Go to the Facebook page and click \"I like\"! http://www.facebook.com/pages/Eddie-Fisher-OneRepublic-F ...";
            Uri expected = new Uri("http://www.facebook.com/pages/Eddie-Fisher-OneRepublic-F");

            UriExtensions.ExtractUris(input).Single().Should().Be(expected);
        }

        [Fact]
        public void UriExtensions_ExtractUris_WithoutHost()
        {
            string input = "{asd:}";
            UriExtensions.ExtractUris(input).Any().Should().BeFalse();
        }

        [Fact]
        public void UriExtensions_IsWeb()
        {
            UriExtensions.IsWeb(new Uri("http://www.google.com")).Should().BeTrue();
            UriExtensions.IsWeb(new Uri("https://www.google.com")).Should().BeTrue();
            UriExtensions.IsWeb(new Uri("ftp://www.google.com")).Should().BeFalse();
            UriExtensions.IsWeb(new Uri("nttp://www.google.com")).Should().BeFalse();
        }

        [Fact]
        public void UriExtensions_ResolveRedirects_Facebook()
        {
            Uri uri = new Uri("http://t.co/6VyBZOO");
            Uri expected = new Uri("https://www.facebook.com/photo.php?pid=585685&l=588142298a&id=100399403341183");

            UriExtensions.ResolveRedirects(uri).Should().Be(expected);
        }

        [Fact]
        public void UriExtensions_ResolveRedirects_ErrorHandling()
        {
            Uri uri = new Uri("http://notfound-asdadagasdg.com");

            Func<WebException, Uri> map = (x) => null;

            UriExtensions.ResolveRedirects(uri, map).Should().BeNull();
        }

        [Fact]
        public void UriExtensions_ResolveRedirects_ThrowsOnNonWeb()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Uri uri = new Uri("ftp://www.google.com");
                UriExtensions.ResolveRedirects(uri);
            });
        }

        [Fact]
        public void UriExtensions_ResolveRedirects_HEADnotAllowed()
        {
            Uri uri = new Uri("http://amzn.to/");
            Uri expected = new Uri(@"http://www.amazon.com/");
            Exception ex = null;
            Uri resolved = UriExtensions.ResolveRedirects(uri, x => { ex = x; return uri; });

            ex.Should().BeNull();
            UriExtensions.ResolveRedirects(uri).Should().Be(expected);
        }

        [Fact(Skip = "tinyurl uses meta tag for redirect ")]
        public void UriExtensions_ResolveRedirects_TinyUrl()
        {
            Uri uri = new Uri("http://tinyurl.com/3lesxnx");
            UriExtensions.ResolveRedirects(uri).Should().Be(new Uri("http://edition.cnn.com/2011/WORLD/europe/07/18/uk.committee.hearing/"));
        }

    }

}
