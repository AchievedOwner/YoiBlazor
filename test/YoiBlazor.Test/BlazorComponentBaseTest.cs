
using Xunit;
using Bunit;
using static Bunit.ComponentParameterFactory;

namespace YoiBlazor.Test
{


    public class BlazorComponentBaseTest
    {
        private readonly TestContext _context;
        public BlazorComponentBaseTest()
        {
            _context = new TestContext();
        }

        [Fact(DisplayName ="CssClassAttribute")]
        public void TestCssClass()
        {
            var component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.Margin),1));
            component.MarkupMatches("<div class=\"m-1\"/>");
        }

        [Fact(DisplayName = "NullCssClassAttribute")]
        public void TestNullCssClass()
        {
            var component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.Margin), 1));
            component.MarkupMatches("<div class=\"m-1\"/>");


            component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.Margin), null));
            component.MarkupMatches("<div />");
        }

        [Fact(DisplayName ="BooleanCssClassAttribute")]
        public void TestBoolCssClassAttribute()
        {
            var component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.Text), true));
            component.MarkupMatches("<div class=\"text\"/>");


            component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.Text), false));
            component.MarkupMatches("<div class=\"text-0\"/>");
        }

        [Fact(DisplayName ="Parameter is CssClass")]
        public void TestCssClassType()
        {
            var component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.Color), Color.Primary));
            component.MarkupMatches("<div class=\"primary\"/>");
        }

        [Fact(DisplayName = "Parameter is CssClass by driving class")]
        public void TestCssClass_WithInheritType()
        {
            var component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.BgColor), BgColor.Primary));
            component.MarkupMatches("<div class=\"bg-primary\"/>");
        }

        [Fact(DisplayName = "Parameter is CssClassCollection")]
        public void TestCssClassCollection()
        {
            var component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.RounedStyles), new CssClassCollection(new[] { "a", "b" })));
            component.MarkupMatches("<div class=\"rounded-a rounded-b\"/>");
        }

        [Fact(DisplayName = "Parameter has StyleAttribute")]
        public void TestStyleAttribute()
        {
            var component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.MaxHeight), 120));
            component.MarkupMatches("<div style=\"max-height:120\"/>");
        }

        [Fact(DisplayName ="ElementReference")]
        public void TestElementReference()
        {
            var compoent= _context.RenderComponent<TestComponent>();
            Assert.NotNull(compoent.Instance.ElementRef.Id);
        }
    }
}
