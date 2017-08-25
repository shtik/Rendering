using Xunit;

namespace Shtik.Rendering.Markdown.Tests
{
    public class ShowRendererTests
    {
        [Fact]
        public void ExtractsShowMetadata()
        {
            var target = new ShowRenderer();
            var actual = target.Render(TestText.Simple);
            Assert.NotNull(actual.Metadata);
            Assert.True(actual.Metadata.TryGetValue("title", out var title));
            Assert.Equal("Pass", title);
            Assert.True(actual.Metadata.TryGetValue("layout", out var layout));
            Assert.Equal("title-and-content", layout);
        }

        [Fact]
        public void ParsesSingleSlide()
        {
            var target = new ShowRenderer();
            var actual = target.Render(TestText.Simple);
            Assert.NotNull(actual.Slides);
            Assert.Equal(1, actual.Slides.Count);
            var slide = actual.Slides[0];

            Assert.NotNull(slide.Metadata);
            Assert.True(slide.Metadata.TryGetValue("layout", out var layout));
            Assert.Equal("title", layout);

            Assert.Equal("<h1>Pass</h1>", slide.Html.Trim());
        }
    }

    internal class TestText
    {
        public const string Simple = @"---
title: Pass
layout: title-and-content
---
layout: title
---
# Pass
---
";
    }
}