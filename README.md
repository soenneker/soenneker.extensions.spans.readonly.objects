[![](https://img.shields.io/nuget/v/soenneker.extensions.spans.readonly.objects.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.spans.readonly.objects/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.spans.readonly.objects/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.spans.readonly.objects/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.spans.readonly.objects.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.spans.readonly.objects/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.spans.readonly.objects/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.spans.readonly.objects/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.Spans.Readonly.Objects
Convert the runtime types in a `ReadOnlySpan<object>` into an array or caller-owned buffer.

## Installation

```bash
dotnet add package Soenneker.Extensions.Spans.Readonly.Objects
```

## Allocate a type array

```csharp
using Soenneker.Extensions.Spans.Readonly.Objects;

ReadOnlySpan<object> arguments = [42, "status", DateTimeOffset.UtcNow];
Type[] argumentTypes = arguments.ToTypes();
```

`ToTypes()` preserves input order and returns an empty array for an empty span. Each type is obtained with `GetType()`, so the result represents runtime types rather than declared types.

## Fill an existing buffer

```csharp
Type[] reusableBuffer = new Type[8];
arguments.FillTypes(reusableBuffer);
```

`FillTypes()` avoids allocating the destination collection. The destination must have at least as many elements as the input; additional elements are left unchanged.

Both methods require every input element to be non-null because `null` has no runtime type. A null element causes `NullReferenceException`.
