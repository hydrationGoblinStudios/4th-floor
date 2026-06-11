using System.Collections;
using UnityEngine;

public class Car : MonoBehaviour
{
    [HideInInspector]
    public int id; // Coloque um ID único para cada carro (1, 2, 3...)

    [HideInInspector] public bool fezMovimentoValido = false;

    public int tamanho = 2; // 2 ou 3 casas
    public bool isHorizontal;
    public bool isMainCar; // Marque como TRUE apenas no carro vermelho

    // Esquerda/Baixo do carro
    public Vector2Int gridPos;

    public enum CarState { Parado, Movendo, Batendo }
    public CarState estado = CarState.Parado;

    private float moveSpeed = 25f;

    void Start()
    {
        // Lê a posição do mundo visual e converte para coordenada da matriz inteira.
        // Seus carros devem ser posicionados em números inteiros na tela (Ex: X=2, Y=3).
        gridPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        GridManager.Instance.RegistrarCarro(this);
    }

    public void TentarMover(int direcao, int casasDesejadas)
    {
        if (estado != CarState.Parado) return;

        int casasLivres = GridManager.Instance.CalcularCasasLivres(this, direcao, casasDesejadas);

        if (casasLivres == 0)
        {
            // AQUI ESTÁ A NOVA REGRA: 
            // Só bate se não houver espaço NENHUM *E* se ainda não tiver andado de verdade neste clique
            if (!fezMovimentoValido)
            {
                StartCoroutine(BatidinhaRoutine(direcao));
            }
        }
        else
        {
            // O carro vai andar de verdade! Então registramos isso na memória.
            fezMovimentoValido = true;
            StartCoroutine(MoverRoutine(direcao, casasLivres));
        }
    }

    IEnumerator MoverRoutine(int direcao, int casasAAndar)
    {
        estado = CarState.Movendo;

        GridManager.Instance.LimparGrid(this);

        if (isHorizontal) gridPos.x += direcao * casasAAndar;
        else gridPos.y += direcao * casasAAndar;

        GridManager.Instance.PreencherGrid(this, gridPos);

        // Animação de deslize
        Vector3 targetPos = new Vector3(gridPos.x, gridPos.y, transform.position.z);
        while (Vector3.Distance(transform.position, targetPos) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        estado = CarState.Parado;

        // Checa se o carro principal chegou na borda direita do grid
        if (isMainCar && gridPos.x + (tamanho - 1) >= 5)
        {
            GridManager.Instance.Vitoria();
        }
    }

    IEnumerator BatidinhaRoutine(int direcao)
    {
        estado = CarState.Batendo;

        Vector3 startPos = transform.position;
        Vector3 bumpPos = startPos;

        // Intensidade do solavanco (0.15 unidades)
        if (isHorizontal) bumpPos.x += direcao * 0.15f;
        else bumpPos.y += direcao * 0.15f;

        // Vai
        while (Vector3.Distance(transform.position, bumpPos) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, bumpPos, (moveSpeed * 0.5f) * Time.deltaTime);
            yield return null;
        }

        // Volta
        while (Vector3.Distance(transform.position, startPos) > 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, (moveSpeed * 0.5f) * Time.deltaTime);
            yield return null;
        }
        transform.position = startPos;

        estado = CarState.Parado;
    }
}