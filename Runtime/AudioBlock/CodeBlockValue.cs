using System;

namespace WaterKat.AudioManagement
{
    public abstract class CodeBlockValue<T>
    {
        public T Value;
        public T Calculate() => Value;
        public abstract void GenerateValue();
        public abstract void GenerateValue(float time);
    }

    public class ConstantBoolCodeBlockValue : CodeBlockValue<bool>
    {
        public override void GenerateValue(){}
        public override void GenerateValue(float time){}
    }

    public class RandomBoolCodeBlockValue : CodeBlockValue<bool>
    {
        public override void GenerateValue() => Value = new Random().Next(2) > 2;
        public override void GenerateValue(float time) { GenerateValue(); }
    }

    public class TimeCurveCodeBlockValue : CodeBlockValue<bool>
    {
        public override void GenerateValue()
        {
            throw new NotImplementedException();
        }

        public override void GenerateValue(float time)
        {
            throw new NotImplementedException();
        }
    }

}
