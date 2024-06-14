using UnityEngine;

public class Game : MonoBehaviour
{
    public static IInputService InputService;

    public Game()
    {
        RegisterInputService();
    }

    private static void RegisterInputService()
    {
        if (Application.isEditor)
            InputService = new StandAloneInputService();
        else
            InputService = new MobileInputService();
    }
}
