# YoiBlazor(用极简的方式构建 Blazor 组件)
如果你想要一个能快速开发出基于已经存在的样式的 Blazor UI 组件库，那么 YoiBlazor 可以使你事半功倍。

Yoi 源自于日语，意思是容易。

#### 构建组件只需3步
1. 继承 `BlazorComponentBase`
2. 在参数上设置 `CssClassAttribute` 特性
3. 组件类打上 `HtmlTag` 特性

### 定义组件
```csharp
[HtmlTag]
public class Container : BlazorComponentBase, IHasChildContent
{
    [Parameter] public RenderFragment ChildContent { get; set; }
    
    [Parameter][CssClass("container-fluid")][FalseCssClass("container")] public bool? Fluid { get; set; }
}
```
### 使用组件
```html
<Container>...</Container>
<Container Fluid="true">...</Container>
```
### 查看HTML
```html
<div class="container">...</div>
<div class="container-fluid">...</div>
```


## 安装(v2.0.0)
> Install-Package YoiBlazor


[查看文档](https://github.com/DotNetStacker/YoiBlazor/wiki)