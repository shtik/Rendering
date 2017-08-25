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

        [Fact]
        public void ParsesEmptyMetadata()
        {
            var target = new ShowRenderer();
            var actual = target.Render(TestText.EmptyMetadata);
            Assert.NotNull(actual.Slides);
            Assert.Equal(1, actual.Slides.Count);
            var slide = actual.Slides[0];

            Assert.NotNull(slide.Metadata);
            Assert.Empty(slide.Metadata);

            Assert.Equal("<h1>Pass</h1>", slide.Html.Trim());
        }

        [Fact]
        public void ParsesMultipleSlides()
        {
            var target = new ShowRenderer();
            var actual = target.Render(TestText.MultipleSlides);
            Assert.NotNull(actual.Slides);
            Assert.Equal(4, actual.Slides.Count);

            // One
            var slide = actual.Slides[0];
            Assert.NotNull(slide.Metadata);
            Assert.True(slide.Metadata.TryGetValue("layout", out var layout));
            Assert.Equal("title", layout);
            Assert.Equal("<h1>One</h1>", slide.Html.Trim());

            // Two
            slide = actual.Slides[1];
            Assert.NotNull(slide.Metadata);
            Assert.Empty(slide.Metadata);
            Assert.Equal("<h1>Two</h1>", slide.Html.Trim());

            // Three
            slide = actual.Slides[2];
            Assert.NotNull(slide.Metadata);
            Assert.True(slide.Metadata.TryGetValue("layout", out layout));
            Assert.Equal("blank", layout);
            Assert.Equal("<h1>Three</h1>", slide.Html.Trim());

            // Four
            slide = actual.Slides[3];
            Assert.NotNull(slide.Metadata);
            Assert.Empty(slide.Metadata);
            Assert.Equal("<h1>Four</h1>", slide.Html.Trim());
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

        public const string EmptyMetadata = @"---
title: Pass
layout: title-and-content
---
---
# Pass
---
";

        public const string MultipleSlides = @"---
title: Pass
layout: title-and-content
---
layout: title
---
# One
---
---
# Two
---
layout: blank
---
# Three
---
---
# Four
---
";
    }
}