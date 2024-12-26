using System.Collections;
using System.Collections.Generic;
using MobileLibrary.Function;
using UnityEngine;
using static MobileLibrary.Function.Function;

namespace  MobileLibrary.Example
{
    public class Example 
    {
        void Main()
        {
          
        }
    }

    public class Sheep
    {
        
    }

    public static class ExampleFunction
    {
        public static Option<Sheep> Father(Sheep sheep)
        {
            return Some(new Sheep());
        }
        
        public static Option<Sheep> Mother(Sheep sheep)
        {
            return Some(new Sheep());
        }
        
        public static Option<Sheep> GrandFather(Option<Sheep> mother)
        {
            //よくない例
            //return mother.Match(None:() => None, Some: sheep => Father(sheep));
            
            //Bindを使った例
            return mother.Bind(Father);
        }
    }
}

