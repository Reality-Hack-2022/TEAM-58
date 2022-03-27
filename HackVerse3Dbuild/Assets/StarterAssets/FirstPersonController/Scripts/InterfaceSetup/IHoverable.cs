using UnityEngine;

public interface IHoverable
{

    bool IsSelected { get; }

      void OnGazeEnter(GazeData data);
    void OnGazeExit(GazeData data);
    void OnClick(GazeData data);
    void SetLooker(GameObject gameObject);
}