using System.Collections.Generic;
using SharpYaml.Serialization;

namespace Shtik.Rendering.Markdown
{
    public class ShowRenderer
    {
        private readonly Serializer _serializer = new Serializer();
        private readonly SlideRenderer _slideRenderer = new SlideRenderer();

        public Show Render(string source)
        {
            var splitter = new Splitter(source);
            var frontMatter = splitter.ReadNextBlock();
            var showMetadata = new Dictionary<string, object>();
            _serializer.DeserializeInto(frontMatter, showMetadata);

            return new Show(showMetadata, RenderSlides(splitter));
        }

        private IEnumerable<Slide> RenderSlides(Splitter splitter)
        {
            while (true)
            {
                var frontMatter = splitter.ReadNextBlock();
                if (frontMatter == null) break;
                var markdown = splitter.ReadNextBlock();
                if (markdown == null) break;
                yield return _slideRenderer.Render(frontMatter, markdown);
            }
        }
    }
}