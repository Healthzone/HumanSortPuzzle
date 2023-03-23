using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseActionSystem : MonoBehaviour
{
    private const int listSize = 5;
    private LinkedList<ReverseElement> _reverseElements = new LinkedList<ReverseElement>();

    public void SaveAction(ReverseElement reverseElement)
    {
        _reverseElements.AddFirst(reverseElement);
        CheckListSize();
    }

    private void CheckListSize()
    {
        if (_reverseElements.Count > listSize)
        {
            _reverseElements.RemoveLast();
        }
    }

    public void ReverseAction()
    {
        if (_reverseElements.Count != 0)
        {
            ReverseElement reverseElement = _reverseElements.First.Value;
            _reverseElements.RemoveFirst();

            FlaskController currentController = reverseElement.Bots[0].GetComponentInParent<FlaskController>();
            if (currentController.IsFilledByOneColor)
            {
                currentController.IsFilledByOneColor = false;
                GetComponent<FinishGameHandler>().CurrentFilledFlaskCount--;
            }

            for (int i = 0; i < reverseElement.Bots.Count; i++)
            {
                var bot = currentController.Bots.Pop();
                currentController.Colors.Pop();
                currentController.ShiftNextPositionIndex(0);
                reverseElement.PreviousFlask.ProcessBotPosition(bot);
            }
        }
    }
}
