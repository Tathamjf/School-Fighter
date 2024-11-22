using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    public float playerSpeed = 1f;

    public Vector2 playerDirection;

    private bool isWalking;
    private Animator playerAnimator;

    private bool playerFaceRight = true;

    void Start()
    {
        // obtem e inicializa as propriedades do Rigidbody2D
        playerRigidBody = GetComponent<Rigidbody2D>();    
        
        // obtem e inicializa as propriedades do Animator
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        UpdadeAnimator();
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

        playerRigidBody.MovePosition(playerRigidBody.position + playerSpeed * Time.fixedDeltaTime * playerDirection);

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
}
