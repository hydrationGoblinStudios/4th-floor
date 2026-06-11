using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    // A nossa matriz 6x6. O 0 representa vazio.
    public int[,] grid = new int[6, 6];
    private int proximoId = 1;

    void Awake()
    {
        // Padrão Singleton para podermos acessar o Grid de qualquer script
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Registra o carro na matriz de acordo com seu tamanho e orientação
    public void RegistrarCarro(Car carro)
    {
        // O GridManager dá um ID único para o carro automaticamente a partir do 1
        carro.id = proximoId;
        proximoId++;

        PreencherGrid(carro, carro.gridPos);
    }

    public void PreencherGrid(Car carro, Vector2Int pos)
    {
        int stepX = carro.isHorizontal ? 1 : 0;
        int stepY = carro.isHorizontal ? 0 : 1;

        for (int i = 0; i < carro.tamanho; i++)
        {
            grid[pos.x + (stepX * i), pos.y + (stepY * i)] = carro.id;
        }
    }

    public void LimparGrid(Car carro)
    {
        int stepX = carro.isHorizontal ? 1 : 0;
        int stepY = carro.isHorizontal ? 0 : 1;

        for (int i = 0; i < carro.tamanho; i++)
        {
            grid[carro.gridPos.x + (stepX * i), carro.gridPos.y + (stepY * i)] = 0;
        }
    }

    // O coração da lógica: conta quantas casas o carro PODE andar naquela direção
    public int CalcularCasasLivres(Car carro, int direcao, int casasDesejadas)
    {
        int casasLivres = 0;
        int stepX = carro.isHorizontal ? direcao : 0;
        int stepY = !carro.isHorizontal ? direcao : 0;

        // Precisamos saber qual é a "ponta" do carro que está avançando
        int startX = carro.gridPos.x + (direcao > 0 && carro.isHorizontal ? carro.tamanho - 1 : 0);
        int startY = carro.gridPos.y + (direcao > 0 && !carro.isHorizontal ? carro.tamanho - 1 : 0);

        for (int i = 1; i <= casasDesejadas; i++)
        {
            int checkX = startX + (stepX * i);
            int checkY = startY + (stepY * i);

            // Se bateu na borda do tabuleiro (parede)
            if (checkX < 0 || checkX >= 6 || checkY < 0 || checkY >= 6) break;

            // Se bateu em outro carro
            if (grid[checkX, checkY] != 0 && grid[checkX, checkY] != carro.id) break;

            casasLivres++;
        }

        return casasLivres;
    }

    // Método modular de vitória
    public void Vitoria()
    {
        Debug.Log("Vitória! O carro vermelho atravessou o tabuleiro.");
        // Coloque aqui o evento ou método que avisa o seu jogo principal que o minigame acabou.
    }
}