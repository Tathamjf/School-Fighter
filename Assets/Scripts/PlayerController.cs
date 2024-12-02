using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    public float playerSpeed = 1f;
    public float currentSpeed;

    public Vector2 playerDirection;

    private bool isWalking;
    private Animator playerAnimator;

    //player olhando para a direita
    private bool playerFaceRight = true;

    //conta a quantidade de ataques simples para o ataque especial
    private int punchCount;

    // conta o tempo para o ataque especial ser executado
    private float timeCross = 0.75f;

    bool comboControll;

    //indica se o player esta morto
     private bool isDead;


    void Start()
    {
        // obtem e inicializa as propriedades do Rigidbody2D
        playerRigidBody = GetComponent<Rigidbody2D>();    
        
        // obtem e inicializa as propriedades do Animator
        playerAnimator = GetComponent<Animator>();

        //inia a velocidade do player
        currentSpeed = playerSpeed;
    }
        // Update is called once per frame
    void Update()
    {
        PlayerMove();
        UpdadeAnimator();


        // jab ataque
        if (Input.GetKeyDown(KeyCode.Q))
        {
           
                // inicializar o temporizador
                StartCoroutine(crossController());

                if (punchCount < 2)
                {
                    PlayerJab();
                    punchCount++;
                }
                
                else if (punchCount >= 2)
                {
                    PlayerCross();
                    punchCount = 0;
                }

                //parando o temporizador
                StopCoroutine(crossController());                      
        }
    }


    // È utilizada geralmente para a implementação de fisica no jogo
    // Oor ter uma execusão padronizada em diferentes dispositivos
    // Acontece a cada inetvalo de tempo = 0,022s
    private void FixedUpdate()
    {
        // verificar se o player esta em movimento
        if (playerDirection.x != 0 || playerDirection.y != 0)
        {
            isWalking = true;

        } else
        {
            isWalking = false;
        }

        playerRigidBody.MovePosition(playerRigidBody.position + currentSpeed * Time.fixedDeltaTime * playerDirection);

    }


    void PlayerMove()
    {
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //se o player vai para a Esquerda e esta olhando para a DIREITA
        if (playerDirection.x < 0 && playerFaceRight)
        {
            Flip();
        }

        //se o player vai pra a Direita e esta olhando para a ESQUERDA
        else if (playerDirection.x > 0 && !playerFaceRight)
        {
            Flip();
        }
    }


    void UpdadeAnimator()
    {
        // definir o valor do parametro do animator, igual a propriedade isWalking
        // parametro entre aspas "IsWalking" é um parametro do Animator - Unity
        // isWalking sem aspas é a variavel criada no codigo acima
        playerAnimator.SetBool("IsWalking", isWalking);
    }


    void Flip()
    {
        // faz girar o player 180g no eixo y

        //inverter o valor da varialver playerFaceRight
        playerFaceRight = !playerFaceRight;

        //girar o sprite do player em 180g
                      // X   Y   Z
        transform.Rotate(0, 180, 0);
    }


    void PlayerJab()
    {
        // acessa a animação IsJab no animator da unity e a torna verdadeira, ativa ela
        playerAnimator.SetTrigger("IsJab");

    }

   
    void PlayerCross()
    {
        playerAnimator.SetTrigger("IsCross");
    }

    //controla o tempo necessario para a exução de um especial
    //corrotina = coisas que acontecem ao mesmo tempo 
    IEnumerator crossController()
    {
        comboControll = true;
        yield return new WaitForSeconds(timeCross);
        punchCount = 0;
        comboControll = false;
    }


    void ZeroSpeed ()
    {
        //zerar a velocidade atual -> currentSpeed
        currentSpeed = 0;
    }

    void ResetSpeed ()
    {
        //resseta a velocidade do player
        currentSpeed = playerSpeed;
    }

}
