// #nullable enable
// using System;
// using FunctionalLibrary;
// using MobileLibrary.Function;
// using static FunctionalLibrary.FunctionalModule;
// using NUnit.Framework;
// using UnityEngine.Assertions;
//
// namespace Tests_EditMode
// {
//     public class OptionTest
//     {
//         [Test]
//         public void Option_Some_ReturnsCorrectValue()
//         {
//             var option1 = Some(1);
//             var val1 = option1.Match(
//                 Some: x => x,
//                 None: () => -1
//             );
//             Assert.That(val1, Is.EqualTo(1));
//
//             var option2 = None<int>();
//             var val2 = option2.Match(
//                 Some: x => x,
//                 None: () => -1
//             );
//             Assert.That(val2, Is.EqualTo(-1));
//         }
//
//         [Test]
//         public void Option_None_ReturnsCorrectState()
//         {
//             var option1 = Some(1);
//             Assert.That(option1.IsSome(), Is.True);
//             Assert.That(option1.IsNone(), Is.False);
//
//             var option2 = None<int>();
//             Assert.That(option2.IsSome(), Is.False);
//             Assert.That(option2.IsNone(), Is.True);
//         }
//
//         [Test]
//         public void Option_Map_TransformsValueCorrectly()
//         {
//             var option = Some(1);
//             var val = option.Map(x => x + 1).Match(
//                 Some: x => x,
//                 None: () => -1
//             );
//             Assert.That(val, Is.EqualTo(2));
//         }
//
//         [Test]
//         public void Option_Bind_ChainsOperationsCorrectly()
//         {
//             var option = Some(1);
//             var val = option.Bind(x => Some(x + 1)).Match(
//                 Some: x => x,
//                 None: () => -1
//             );
//             Assert.That(val, Is.EqualTo(2));
//
//             Option<int> AddWhenEven(int x) => x % 2 == 0 ? Some(x + 1) : None<int>();
//             var val2 = option.Bind(AddWhenEven).Match(
//                 Some: x => x,
//                 None: () => -1
//             );
//             Assert.That(val2, Is.EqualTo(-1));
//         }
//
//     }
//
//     public class TestAttribute : Attribute
//     {
//     }
// }