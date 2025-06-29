using System;




namespace Expansion
{
    public class Compatibility
    {


        public struct CompatibilityData
        {
            public CompatibilityData(string _classTag = null, string _variableTag = null, string _methodTag = null)
            {
                classTag = null;
                variableTag = null;
                methodTag = null;

                if (_classTag.IsExist())
                {
                    classTag = _classTag;
                    return;
                }
                else if (_variableTag.IsExist())
                {
                    variableTag = _variableTag;
                    return;
                }
                else if (_methodTag.IsExist())
                {
                    methodTag = _methodTag;
                }
            }


            public readonly string classTag;
            public readonly string variableTag;
            public readonly string methodTag;
        }




        [AttributeUsage(AttributeTargets.Class)]
        public class Class : Attribute
        {
            public Class(string _tag)
            {
                compatibility = new CompatibilityData(_classTag: _tag);
            }


            public readonly CompatibilityData compatibility;
        }




        [AttributeUsage(AttributeTargets.Field)]
        public class Variable : Attribute
        {
            public Variable(string _tag)
            {
                compatibility = new CompatibilityData(_variableTag: _tag);
            }


            public readonly CompatibilityData compatibility;
        }
    }
}