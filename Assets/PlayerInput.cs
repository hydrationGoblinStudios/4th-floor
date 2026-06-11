using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Car carroSelecionado;
    private Vector2 posicaoMouseInicial;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 1. Jogador APERTA o clique
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Car carroClicado = hit.collider.GetComponent<Car>();

                if (carroClicado != null && carroClicado.estado == Car.CarState.Parado)
                {
                    carroSelecionado = carroClicado;

                    // AQUI ESTÁ O RESET: Apagamos a memória do clique passado
                    carroSelecionado.fezMovimentoValido = false;

                    posicaoMouseInicial = mousePos;
                }
            }
        }

        // 2. Jogador SOLTA o clique
        if (Input.GetMouseButtonUp(0))
        {
            carroSelecionado = null; // Desseleciona o carro na hora
        }

        // 3. Jogador ARRASTA o mouse
        if (Input.GetMouseButton(0) && carroSelecionado != null)
        {
            // Se o carro começou a se mover/bater, pausamos a leitura deste arrasto
            if (carroSelecionado.estado != Car.CarState.Parado) return;

            Vector2 mousePosAtual = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dragDelta = mousePosAtual - posicaoMouseInicial;

            // Focamos apenas no eixo do carro
            float arrasto = carroSelecionado.isHorizontal ? dragDelta.x : dragDelta.y;

            int direcao = arrasto > 0 ? 1 : -1;

            // A sua regra: 0.8 = 0 casas, 1.2 = 1 casa, 2.5 = 2 casas
            int casasDesejadas = Mathf.FloorToInt(Mathf.Abs(arrasto));

            // Chegou no gatilho mínimo para virar um comando
            if (casasDesejadas >= 1)
            {
                carroSelecionado.TentarMover(direcao, casasDesejadas);

                // Puxamos a âncora do mouse para frente. Isso permite que se o jogador 
                // continuar arrastando a tela de uma ponta à outra sem soltar o clique, 
                // o carro vai continuar andando assim que a animação dele parar.
                if (carroSelecionado.isHorizontal)
                    posicaoMouseInicial.x += direcao * casasDesejadas;
                else
                    posicaoMouseInicial.y += direcao * casasDesejadas;
            }
        }
    }
}