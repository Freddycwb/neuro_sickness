using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDialog : MonoBehaviour
{
    public GameObjectVariable player;
    public GameObjectArrayVariable inventory;
    public float timeStucked;
    public bool talking;

    private bool _tipPassPickUp;
    private bool _endPickUpTutorial, _endInteractTutorial, _endChangeItemTutorial;

    private void Update()
    {
        if (!_endPickUpTutorial)
        {
            PickUpItemTutorial();
        }
    }

    private void PickUpItemTutorial()
    {
        if (!talking)
        {
            timeStucked += Time.deltaTime;
        }
        if (timeStucked > 5)
        {
            GetComponent<AIDialogs>().Speech("Usuario, parece que aquele cartão pode destravar a porta, clique sobre ele estando perto para pega-lo");
            timeStucked = -3;
            _tipPassPickUp = true;
        }
    }

    public void InteractTutorial()
    {
        if (!_endInteractTutorial)
        {
            _endPickUpTutorial = true;
            if (_tipPassPickUp)
            {
                GetComponent<AIDialogs>().Speech("Agora para usa-lo, aproxime-se da porta e aperte a tecla Espaço");
            }
            else
            {
                GetComponent<AIDialogs>().Speech("Usuario, esse cartão parece poder abrir aquela porta, aproxime-se dela e aperte a tecla Espaço para usa-lo");
            }
        }
    }

    public void OpenFirstDoor()
    {
        if (!_endInteractTutorial)
        {
            _endInteractTutorial = true;
            GetComponent<AIDialogs>().Speech("O sinal indica que tem um painel de controle proximo.Caso voce o ative o metro ficaria parcialmente operacional");
        }
    }

    public void TooFar()
    {
        GetComponent<AIDialogs>().Speech("O Usuario esta muito longe para fazer isso");
    }

    public void Full()
    {
        GetComponent<AIDialogs>().Speech("O Usuario esta com o inventario cheio, clique sobre os itens no inventario para liberar espaço");
    }

    public void ChangeItemTutorial()
    {
        if (inventory.Value[0] != null && !_endChangeItemTutorial)
        {
            GetComponent<AIDialogs>().Speech("O usuario esta carregando mais de um item no inventario, para alternar entre eles use as teclas 1 2 3");
            _endChangeItemTutorial = true;
        }
    }

    public void DangerArea()
    {
        GetComponent<AIDialogs>().Speech("Esse lugar é perigoso para soltar um item, voce pode não conseguir recupera-lo mais");
    }

    public void AIStartTalk()
    {
        talking = true;
    }

    public void AIStopTalk()
    {
        talking = false;
    }
}
