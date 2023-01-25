using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private UIDocument mainMenu;
    private Button playBtn;
    private Button quitBtn;
    private VisualElement root;
    
    // Start is called before the first frame update
    void Start()
    {
        root = mainMenu.rootVisualElement;

        playBtn = root.Q<Button>("play");
        quitBtn = root.Q<Button>("quit");

        playBtn.clicked += PlayGame;
        quitBtn.clicked += QuitGame;
        
        ResponsiveBindings();
    }

    // Update is called once per frame
    void Update()
    {
        ResponsiveBindings();
    }

    void ResponsiveBindings()
    {
        StyleLength height = new StyleLength();
        StyleLength width = new StyleLength();
        StyleLength fontSize = new StyleLength();
        float heightValue = (float)(root.layout.height * .07);
        height.value = new Length(heightValue, root.style.height.value.unit);
        width.value = new Length(heightValue * 3, root.style.width.value.unit);
        fontSize.value = new Length((float) (heightValue * .5), root.style.width.value.unit);

        playBtn.style.height = height;
        playBtn.style.width = width;
        playBtn.style.fontSize = fontSize;
        quitBtn.style.height = height;
        quitBtn.style.width = width;
        quitBtn.style.fontSize = fontSize;

    }

    void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Rogue");
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
