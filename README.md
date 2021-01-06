# YoiBlazor
The simpliest way to build the component.

### Build component
```csharp
[HtmlTag]
public class Container : BlazorComponentBase, IHasChildContent
{
    [Parameter] public RenderFragment ChildContent { get; set; }
    
    [Parameter][CssClass("container-fluid")][FalseCssClass("container")] public bool? Fluid { get; set; }
}
```
### Use component
```html
<Container>...</Container>
<Container Fluid="true">...</Container>
```
### Check HTML
```html
<div class="container">...</div>
<div class="container-fluid">...</div>
```


## Installation(v2.1.0)
> Install-Package YoiBlazor


### [See Document](https://github.com/AchievedOwner/YoiBlazor/wiki)