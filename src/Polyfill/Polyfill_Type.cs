// <auto-generated />
#pragma warning disable

namespace Polyfills;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Link = System.ComponentModel.DescriptionAttribute;

static partial class Polyfill
{
#if NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2_0
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo.hassamemetadatadefinitionas")]
    public static bool HasSameMetadataDefinitionAs(this MemberInfo target, MemberInfo other) =>
        target.MetadataToken == other.MetadataToken &&
        target.Module.Equals(other.Module);
#endif

    /// <summary>
    /// Gets a value that indicates whether the current Type represents a type parameter in the definition of a generic method.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.type.isgenericmethodparameter")]
    public static bool IsGenericMethodParameter(this Type target) =>
#if NETFRAMEWORK || NETSTANDARD2_0 || NETCOREAPP2_0
        target.IsGenericParameter &&
        target.DeclaringMethod != null;
#else
        target.IsGenericMethodParameter;
#endif

    /// <summary>
    /// Generic version of Type.IsAssignableTo https://learn.microsoft.com/en-us/dotnet/api/system.type.isassignableto.
    /// </summary>
    public static bool IsAssignableTo<T>(this Type target) =>
        typeof(T).IsAssignableFrom(target);

    /// <summary>
    /// Generic version of Type.IsAssignableFrom https://learn.microsoft.com/en-us/dotnet/api/system.type.isassignablefrom.
    /// </summary>
    public static bool IsAssignableFrom<T>(this Type target) =>
        target.IsAssignableFrom(typeof(T));

#if NETFRAMEWORK || NETSTANDARD || NETCOREAPPX
    /// <summary>
    /// Determines whether the current type can be assigned to a variable of the specified targetType.
    /// </summary>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.type.isassignableto")]
    public static bool IsAssignableTo(this Type target, [NotNullWhen(true)] Type? targetType) =>
        targetType?.IsAssignableFrom(target) ?? false;
#endif

#if !NET6_0_OR_GREATER

    /// <summary>
    /// Searches for the MemberInfo on the current Type that matches the specified MemberInfo.
    /// </summary>
    /// <param name="type">The MemberInfo to find on the current Type.</param>
    /// <param name="member">The MemberInfo to find on the current Type.</param>
    /// <returns>An object representing the member on the current Type that matches the specified member.</returns>
    [Link("https://learn.microsoft.com/en-us/dotnet/api/system.type.getmemberwithsamemetadatadefinitionas")]
    internal static MemberInfo GetMemberWithSameMetadataDefinitionAs(
        this Type type,
        MemberInfo member)
    {
        const BindingFlags all = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
        foreach (var info in type.GetMembers(all))
        {
            if (info.HasSameMetadataDefinitionAs(member))
            {
                return info;
            }
        }

        throw new MissingMemberException(type.FullName, member.Name);
    }

#endif
}