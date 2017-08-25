using System.Threading.Tasks;
using Xunit;

namespace Shtik.Rendering.Markdown.Tests
{
    public class SplitterTests
    {
        [Fact]
        public void IgnoresLeadingTripleDash()
        {
            const string text = "---\nPass\n---";
            using (var target = new Splitter(text))
            {
                var actual = target.ReadNextBlock();
                Assert.Equal("Pass", actual);
            }
        }

        [Fact]
        public void SplitsAtTripleDash()
        {
            const string text = "---\nOne\n---\n---\nTwo\n---";
            using (var target = new Splitter(text))
            {
                var one = target.ReadNextBlock();
                var two = target.ReadNextBlock();
                Assert.Equal("One", one);
                Assert.Equal("Two", two);
            }
        }

        [Fact]
        public void DoesNotNeedTripleDashAtEnd()
        {
            const string text = "---\nOne\n---\n---\nTwo";
            using (var target = new Splitter(text))
            {
                var one = target.ReadNextBlock();
                var two = target.ReadNextBlock();
                Assert.Equal("One", one);
                Assert.Equal("Two", two);
            }
        }
    }
}
