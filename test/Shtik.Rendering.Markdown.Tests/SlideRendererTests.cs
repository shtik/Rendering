using Xunit;

namespace Shtik.Rendering.Markdown.Tests
{
    public class SlideRendererTests
    {
        [Fact]
        public void RendersYaml()
        {
            const string frontMatter = "title: Pass";
            const string markdown = "# Pass";
            var target = new SlideRenderer();
            var actual = target.Render(frontMatter, markdown);
            Assert.Equal("Pass", actual.Metadata["title"]);
        }

        [Fact]
        public void RendersH1Tag()
        {
            const string frontMatter = "title: Pass";
            const string markdown = "# Pass";
            var target = new SlideRenderer();
            var actual = target.Render(frontMatter, markdown);
            Assert.Equal("<h1>Pass</h1>", actual.Html.Trim());
        }

        [Fact]
        public void DisablesScriptTags()
        {
            const string frontMatter = "title: Pass";
            const string markdown = "<script>hack();</script>";
            var target = new SlideRenderer();
            var actual = target.Render(frontMatter, markdown);
            Assert.Equal("&lt;script&gt;hack();&lt;/script&gt;", actual.Html.Trim());
        }

        [Fact]
        public void LeavesArbitraryTags()
        {
            const string frontMatter = "title: Pass";
            const string markdown = "<sub>2</sub><script>hack();</script>";
            var target = new SlideRenderer();
            var actual = target.Render(frontMatter, markdown);
            Assert.Equal("<p><sub>2</sub>&lt;script&gt;hack();&lt;/script&gt;</p>", actual.Html.Trim());
        }
    }
}