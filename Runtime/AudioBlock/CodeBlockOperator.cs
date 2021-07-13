using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterKat.AudioManagement
{
    public abstract class CodeBlockOperator<T>
    {
        public abstract T Calculate(T inputValue);
    }

    public class NotCodeBlockOperator : CodeBlockOperator<bool>
    {
        public override bool Calculate(bool inputValue)
        {
            return !inputValue;
        }
    }
}
