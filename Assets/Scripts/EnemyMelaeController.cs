using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    //indica se o inimigo esta vivo
    public bool isdeath;

    //variaves para controlar o lado que o inimigo esta virado    
    public bool facingRight;
    public bool previousDirectionRight;

    //variavel para armazenar a posição do player
    private Transform target;

    //variaveis para a movimentação do inimigo
    private float enemySpeed = 0.3f;
    private float currentSpeed;

    private bool isWalking;

    private float horizontalForce;
    private float verticalForce;

    //variavel que vamos usar para controlar o intervalo de tempo que o inimigo ficará andando
    private float walkTimer;

       
    void Start()
    {
        //istanciando o RigidBody do Enemy
        rb = GetComponent<Rigidbody2D>();
        //instanciando o Animator do Enemy
        animator = GetComponent<Animator>();

        //buscar o player e armazenar sua posição
        target = FindAnyObjectByType<PlayerController>().transform;  

        //inicializar a velocidade do inimigo
        currentSpeed = enemySpeed;
        
    }

    
    void Update()
    {
        //verificar se o player esta para a direita ou para a esquerda
        //E determinar o lado que o inimigo ficara virado
        if (target.position.x < this.transform.position.x)            
        {
            facingRight = false;
        }else
        {
            facingRight = true;
        }

        // se facingRight for TRUE vamos flipar o inimigo
        // se n vamos virar o inimigo para a esquerda (posição original)
        //se o player esta a direita  e a direcao amterior NAO era direita (inimigo olhando para esquerda
        if (facingRight && !previousDirectionRight)
        {
            this.transform.Rotate(0, 180, 0);
            previousDirectionRight = true;
        }

        //se o player NAO esta a direita  e a direcao amterior era direita (inimigo olhando para direita
        if (!facingRight && previousDirectionRight)
        {
            this.transform.Rotate(0, -180, 0);
            previousDirectionRight = false;
        }

        //iniciar o timer do inimigo -> retorna o momento atual, conta o tempo do jogo
        walkTimer += Time.deltaTime;

        //Gerenciar a animação de Walking do inimigo
        if (horizontalForce == 0 && verticalForce == 0)
        {
            isWalking = false;
        }else
        {
            isWalking = true;
        }

        UpdadeAnimator();

    }

    private void FixedUpdate()
    {
        //MOVIMENTAÇÃO

        //variavel armazenar a distancia entre o inimigo e o player        
        Vector3 targetDistance = target.position - this.transform.position;

        //Detrmna se a força horizontal deve ser positiva o negativa
        //se a distancia é positiva: 5 / 5 = 1
        //se a distancia for negativa: -5 / 5 = -1
        horizontalForce = targetDistance.x / Math.Abs(targetDistance.x);

        //entre 1 e 2 segundos sera feita uma definição de direção vertical
        if (walkTimer >= UnityEngine.Random.Range(1f, 2f))
        {
            verticalForce = UnityEngine.Random.Range(-1, 2);

            //Zera o timer de movimentação para andar verticalmente novamente daqui +/- 1seg
            walkTimer = 0;
        }


        // caso esteja perto do player, parar a movimentação
        if (Math.Abs(targetDistance.x) < 0.2)
        {
            horizontalForce = 0;
        }

        //aplica a velocidade do inimigo fazendo ele se movimentar
        rb.linearVelocity = new Vector2(horizontalForce * currentSpeed, verticalForce * currentSpeed);
    }


    void UpdadeAnimator()
    {
        animator.SetBool("IsWalking", isWalking);
    }
}
