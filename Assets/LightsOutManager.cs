using UnityEngine;

public class LightsOutManager : MonoBehaviour
{
    public static LightsOutManager Instance;

    // Matriz 10x10 que guarda as referências dos painéis
    private LightPanel[,] paineis = new LightPanel[10, 10];
    private bool jogoFinalizado = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Chamado por cada painel ao iniciar o jogo
    public void RegistrarPainel(LightPanel painel)
    {
        // Proteção para garantir que o painel está dentro do grid 10x10
        if (painel.x >= 0 && painel.x < 10 && painel.y >= 0 && painel.y < 10)
        {
            paineis[painel.x, painel.y] = painel;
        }
    }

    public void ProcessarClique(int x, int y)
    {
        if (jogoFinalizado) return;

        // Inverte o painel clicado e os 4 vizinhos adjacentes
        AlternarPainel(x, y);
        AlternarPainel(x + 1, y); // Direita
        AlternarPainel(x - 1, y); // Esquerda
        AlternarPainel(x, y + 1); // Cima
        AlternarPainel(x, y - 1); // Baixo

        VerificarVitoria();
    }

    private void AlternarPainel(int x, int y)
    {
        // Checagem de Limites (Boundary Check): Evita erros ao clicar nas bordas
        if (x >= 0 && x < 10 && y >= 0 && y < 10)
        {
            if (paineis[x, y] != null)
            {
                paineis[x, y].AlternarEstado();
            }
        }
    }

    private void VerificarVitoria()
    {
        // A sua regra: Vitória ocorre quando TODAS as luzes estão ACESAS
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                // Se algum painel estiver apagado (ou se faltar painel no grid), ainda não ganhou
                if (paineis[x, y] != null && !paineis[x, y].isLit)
                {
                    return;
                }
            }
        }

        // Se passar por todos os 100 espaços e não achar nenhum apagado, é vitória
        Vitoria();
    }

    private void Vitoria()
    {
        jogoFinalizado = true;
        Debug.Log("Vitória! Todos os painéis estão acesos.");
        // Integração futura com o jogo principal entra aqui
    }
}