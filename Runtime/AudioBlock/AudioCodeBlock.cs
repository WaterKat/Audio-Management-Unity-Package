using System;

namespace WaterKat.AudioManagement
{
    [Serializable]
    public abstract class AudioCodeBlock<T>
    {
        
        public abstract T CalculateValue(T inputValue);
    }

    public class PassthroughFloatCodeBlock: AudioCodeBlock<float>
    {
        public override float CalculateValue(float inputValue)
        {
            return inputValue;
        }
    }

    public class ConstantCondition : AudioCodeBlock<float>
    {
        public override float CalculateValue(float inputValue)
        {
            throw new NotImplementedException();
        }
    }
}
