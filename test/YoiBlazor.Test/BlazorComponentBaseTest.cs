
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

        [Fact(DisplayName ="CssClassAttribute 特性")]
        public void TestCssClass()
        {
            var component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.Margin),1));
            component.MarkupMatches("<div class=\"m-1\"/>");
        }

        [Fact(DisplayName = "NullCssClassAttribute 特性")]
        public void TestNullCssClass()
        {
            var component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.Margin), 1));
            component.MarkupMatches("<div class=\"m-1\"/>");


            component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.Margin), null));
            component.MarkupMatches("<div />");
        }

        [Fact(DisplayName ="BooleanCssClassAttribute 特性")]
        public void TestBoolCssClassAttribute()
        {
            var component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.Text), true));
            component.MarkupMatches("<div class=\"text\"/>");


            component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.Text), false));
            component.MarkupMatches("<div class=\"text-0\"/>");
        }

        [Fact(DisplayName ="参数是 CssClass 类型")]
        public void TestCssClassType()
        {
            var component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.Color), Color.Primary));
            component.MarkupMatches("<div class=\"primary\"/>");
        }

        [Fact(DisplayName ="参数是 CssClass 的派生类")]
        public void TestCssClass_WithInheritType()
        {
            var component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.BgColor), BgColor.Primary));
            component.MarkupMatches("<div class=\"bg-primary\"/>");
        }

        [Fact(DisplayName ="参数是 CssClassCollection 类型")]
        public void TestCssClassCollection()
        {
            var component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.RounedStyles), new CssClassCollection(new[] { "a", "b" })));
            component.MarkupMatches("<div class=\"rounded-a rounded-b\"/>");
        }

        [Fact(DisplayName ="参数标记了 StyleAttribute")]
        public void TestStyleAttribute()
        {
            var component = _context.RenderComponent<TestComponent>(Parameter(nameof(TestComponent.MaxHeight), 120));
            component.MarkupMatches("<div style=\"max-height:120\"/>");
        }

        [Fact(DisplayName ="ElementReference 属性")]
        public void TestElementReference()
        {
            var compoent= _context.RenderComponent<TestComponent>();
            Assert.NotNull(compoent.Instance.ElementRef.Id);
        }
    }
}
