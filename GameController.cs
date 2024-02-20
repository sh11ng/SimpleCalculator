using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] TMP_Text Display;
    [SerializeField] TMP_Text NegDisplay;
    [SerializeField] TMP_Text AnsDisplay;

    [SerializeField] private string current_String;

    [SerializeField] private int i;
    private int j;

    [SerializeField] private double _answer;

    [SerializeField] private double[] number = new double[32];
    [SerializeField] private char[] _operator = new char[32];

    [SerializeField] private bool IsNeg;
    [SerializeField] private bool IsOperator;
    [SerializeField] private bool IsAnswer;
    [SerializeField] private bool IsClear;
    [SerializeField] private bool AnswerOn;

    void Start()
    {
        Display.text = string.Empty;
        current_String = string.Empty;
        AnsDisplay.text = string.Empty;
        IsNeg = false;
        IsOperator = true;
        IsAnswer = false;
        IsClear = true;
        AnswerOn = false;

        i = 0;
        j = 0;
        _answer = 0;
    }

    void Math()
    {
        OrderOfOperation();

        int cycle = 0;
        while (cycle != i)
        {
            if (cycle == 0 && IsNeg == true)
            {
                switch (_operator[cycle])
                {
                    case ('+'):
                        _answer = -number[cycle] + number[cycle + 1];
                        number[cycle + 1] = _answer;
                        break;

                    case ('-'):
                        _answer = -number[cycle] - number[cycle + 1];
                        number[cycle + 1] = _answer;
                        break;
                }
            }
            else
            {
                switch (_operator[cycle])
                {
                    case ('+'):
                        _answer = number[cycle] + number[cycle + 1];
                        number[cycle + 1] = _answer;
                        break;

                    case ('-'):
                        _answer = number[cycle] - number[cycle + 1];
                        number[cycle + 1] = _answer;

                        break;
                }
            }

            cycle++;
        }
    }

    public void AddToString(string val)
    {
        if (AnswerOn == true && i == 0)
        {
            return;
        }

        if (IsAnswer == true)
        {
            return;
        }

        Display.text += val.ToString();
        current_String += val.ToString();
        AnsDisplay.text = string.Empty;
        IsOperator = false;
    }

    public void AddToArray(string Operator)
    {
        if(IsOperator == true)
        {
            return;
        }
        else if (IsAnswer == true)
        {
            switch (Operator)
            {
                case ("x"):
                    Display.text += Operator.ToString();
                    _operator[i] = char.Parse(Operator);
                    break;

                case ("+"):
                    Display.text += Operator.ToString();
                    _operator[i] = char.Parse(Operator);
                    break;

                case ("/"):
                    Display.text += Operator.ToString();
                    _operator[i] = char.Parse(Operator);
                    break;

                case ("-"):
                    Display.text += Operator.ToString();
                    _operator[i] = char.Parse(Operator);
                    break;
            }

            AnsDisplay.text = string.Empty;
            IsAnswer = false;
            IsOperator = true;
            i++;
            return;
        }

        switch (Operator)
        {
            case ("x"):
                Display.text += Operator.ToString();
                number[i] = double.Parse(current_String);
                _operator[i] = char.Parse(Operator);
                current_String = string.Empty;
                break;

            case ("+"):
                Display.text += Operator.ToString();
                number[i] = double.Parse(current_String);
                _operator[i] = char.Parse(Operator);
                current_String = string.Empty;
                break;

            case ("/"):
                Display.text += Operator.ToString();
                number[i] = double.Parse(current_String);
                _operator[i] = char.Parse(Operator);
                current_String = string.Empty;
                break;

            case ("-"):
                Display.text += Operator.ToString();
                number[i] = double.Parse(current_String);
                _operator[i] = char.Parse(Operator);
                current_String = string.Empty;
                break;
        }

        AnsDisplay.text = string.Empty;
        IsAnswer = false;
        IsOperator = true;
        i++;
    }

    public void AddToAnswer()
    {
        if(IsAnswer == true && IsClear == false)
        {
            _operator[i] = 'x';
            i++;
            number[i] = _answer;
            Display.text += "Ans";
            IsAnswer = true;
            IsOperator = false;
        }

        else if(IsAnswer == false && IsClear == false)
        {
            number[i] = _answer;
            Display.text += "Ans";
            IsAnswer = true;
            IsOperator = false;
        }
    }

    public void TotalUp()
    {
        if(IsOperator == true)
        {
            return;
        }

        if(IsAnswer == false)
        {
            number[i] = float.Parse(current_String);
        }

        Math();

        i = 0;
        current_String = string.Empty;
        Display.text = string.Empty;
        Display.text += "Ans";
        AnsDisplay.text = _answer.ToString();
        number[i] = _answer;
        IsAnswer = true;
        IsNeg = false;
        IsClear = false;
        AnswerOn = true;
        NegDisplay.enabled = false;
    }

    public void NegString()
    {
        if(IsNeg == false)
        {
            IsNeg = true;
            NegDisplay.enabled = true;
        }

        else if (IsNeg == true)
        {
            IsNeg &= false;
            NegDisplay.enabled = false;
        }
    }

    public void RemoveCharString()
    {
        if (i == 0 && current_String.Length == 0)
        {
            return;
        }

        if (AnswerOn == true && i == 0)
        {
            return;
        }

        if (IsOperator == true)
        {
            i--;
            Display.text = Display.text.Remove(Display.text.Length - 1, 1);
            current_String = number[i].ToString();
            IsOperator = false;
            return;
        }

        Display.text = Display.text.Remove(Display.text.Length - 1, 1);
        current_String = current_String.Remove(current_String.Length - 1, 1);

        if (current_String.Length == 0)
        {
            IsOperator = true;
        }
    }

    public void StringClear()
    {
        Display.text = string.Empty;
        current_String = string.Empty;
        AnsDisplay.text = string.Empty;
        IsAnswer = false;
        IsNeg = false;
        IsOperator = false;
        IsClear = true;
        AnswerOn = false;

        i = 0;
        j = 0;
        _answer = 0;
    }

    public void OrderOfOperation()
    {
        if(AnswerOn == true && i == 0)
        {
            return;
        }

        while (j <= i)
        {
            if (j == 0 && IsNeg == true)
            {
                if (_operator[j] == 'x' || _operator[j] == '/')
                {
                    if (_operator[j] == 'x')
                    {
                        number[j] = -number[j] * number[j + 1];
                        _answer = number[j];
                        _operator[j] = _operator[j + 1];
                    }
                    else if (_operator[j] == '/')
                    {
                        number[j] = -number[j] / number[j + 1];
                        _answer = number[j];
                        _operator[j] = _operator[j + 1];
                    }

                    while (j <= i)
                    {
                        j++;
                        number[j] = number[j + 1];
                        _operator[j] = _operator[j + 1];
                    }

                    i--;
                    j = 0;
                    IsNeg = false;
                }
            }

            else if (_operator[j] == 'x' || _operator[j] == '/')
            {
                 if (_operator[j] == 'x')
                 {
                    number[j] = number[j] * number[j + 1];
                    _answer = number[j];
                    _operator[j] = _operator[j + 1];
                 }
                 else if (_operator[j] == '/')
                 {
                    number[j] = number[j] / number[j + 1];
                    _answer = number[j];
                    _operator[j] = _operator[j + 1];
                 }

                 while (j <= i)
                 {
                    j++;
                    number[j] = number[j + 1];
                    _operator[j] = _operator[j + 1];
                 }

                 i--;
                 j = 0;
            }

            j++;
        }

        j = 0;
    }
}