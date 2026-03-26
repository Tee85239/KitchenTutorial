using System;
using UnityEngine;

public interface IProgressBar
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public event EventHandler<OnProgressChangedEventArgs> onProgressChange;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

}
