using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "NaturalNumberValidator", menuName = "Input Validators/Natural Number Validator", order = 1)]// 이 속성은 Unity 에디터에서 이 클래스를 생성할 수 있는 메뉴 항목을 추가합니다. 파일 이름과 메뉴 이름, 순서를 지정합니다.
public class NaturalNumberValidator : TMP_InputValidator// 이 클래스는 TMP_InputValidator를 상속받아 자연수(0-9)만 입력할 수 있도록 하는 유효성 검사기를 구현합니다.
{
    // 이 메서드는 입력된 문자가 자연수(0-9)인지 확인합니다.
    // ref 키워드는 해당 매개변수가 참조로 전달됨을 나타냅니다. 즉, 메서드 내에서 이 매개변수의 값을 변경하면 호출한 곳에서도 변경된 값이 반영됩니다.
    // 오버라이드를 해야 기존의 Validate 메서드를 재정의할 수 있습니다.
    // text와 pos는 입력된 텍스트와 현재 위치를 나타냅니다. 현재의 구현에서는 사용되지 않습니다.
    // ch는 입력된 문자입니다.
    public override char Validate(ref string text, ref int pos, char ch)
    {
        if (ch >= '0' && ch <= '9')//ch가 '0'에서 '9' 사이의 숫자인지 확인합니다. 만약 그렇다면, 해당 문자를 반환합니다.
        {
            return ch;// 입력된 문자를 텍스트에 추가합니다.
        }
        return '\0'; // 숫자가 아니면 null 문자 반환 (입력 무시)
    }
}