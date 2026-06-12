using UnityEngine;

public class LightPanel : MonoBehaviour
{
    [HideInInspector] public int x, y;

    // Você vai marcar isso como TRUE ou FALSE no Inspector ao criar a fase
    public bool isLit = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Pega a posição na tela e converte para coordenada da matriz, igual aos carros
        x = Mathf.RoundToInt(transform.position.x);
        y = Mathf.RoundToInt(transform.position.y);

        LightsOutManager.Instance.RegistrarPainel(this);
        AtualizarVisual();
    }

    public void AlternarEstado()
    {
        isLit = !isLit;
        AtualizarVisual();
    }

    private void AtualizarVisual()
    {
        // Mude as cores aqui para bater com a arte do seu jogo
        spriteRenderer.color = isLit ? Color.yellow : Color.gray;
    }

    // Método nativo da Unity que dispensa a criação de um script de Input
    void OnMouseDown()
    {
        LightsOutManager.Instance.ProcessarClique(x, y);
    }
}