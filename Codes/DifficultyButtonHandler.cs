using UnityEngine;

public class DifficultyButtonHandler : MonoBehaviour
{
    public void SetClassic() => DifficultySettings.SetClassic();
    public void SetEasy() => DifficultySettings.SetEasy();
    public void SetNormal() => DifficultySettings.SetNormal();
    public void SetHard() => DifficultySettings.SetHard();
}
