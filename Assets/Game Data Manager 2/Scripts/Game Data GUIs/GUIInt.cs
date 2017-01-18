using UnityEngine;
using System.Collections;

namespace GameDataManager
{
    public class GUIInt : GUIObject
    {
        private int integer = 0;
        private int minValue = -1;
        private int maxValue = -1;
        public bool isSlider { get; set; }
        public bool hasMinValue { get; set; }

        public new int value
        {
            get
            {
                return integer;
            }
            set
            {
                if (isSlider)
                {
                    integer = Mathf.Clamp(value, minValue, maxValue);
                }
                else if (hasMinValue && value < minValue)
                {
                    integer = minValue;
                }
                else
                {
                    integer = value;
                }
            }
        }

        public int MinValue
        {
            get
            {
                return minValue;
            }
            set
            {
                minValue = value;
                integer = minValue;
            }
        }
        public int MaxValue
        {
            get
            {
                return maxValue;
            }
            set
            {
                maxValue = value;
            }
        }

        public GUIInt() { }

        public GUIInt(string label)
        {
            this.label = label;
            isSlider = false;
        }

        public GUIInt(string label, int minValue)
        {
            this.label = label;
            this.hasMinValue = true;
            this.MinValue = minValue;
        }

        public GUIInt(string label, bool isSlider, int minValue, int maxValue)
        {
            this.label = label;
            this.isSlider = isSlider;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public override GUIObject Copy()
        {
            GUIInt copy = new GUIInt();
            copy.label = this.label;
            copy.value = this.value;
            copy.integer = this.integer;
            copy.minValue = this.minValue;
            copy.maxValue = this.maxValue;
            copy.isSlider = this.isSlider;
            copy.hasMinValue = this.hasMinValue;
            return copy;
        }

        public override string ToString()
        {
            return string.Format("[GUIInt]{0}:{1}",label,value);
        }
    }
}
