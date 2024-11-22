using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // se pressionar qualquer tecla
        if (Input.GetKeyDown(KeyCode.Return))
        {   //muda a cena
            StartCoroutine(CarregarFase("Fase1"));
        }

    }

    //metodo avançado! corrotina - coroutine

    IEnumerator CarregarFase(string nomeFase)
    {
        //iniciar a animação
        transition.SetTrigger("Start");

        //esperar o tempo de animação
        yield return new WaitForSeconds(transitionTime);

        //carregar a cena
        SceneManager.LoadScene(nomeFase);
    }

}
