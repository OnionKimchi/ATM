using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "NaturalNumberValidator", menuName = "Input Validators/Natural Number Validator", order = 1)]// �� �Ӽ��� Unity �����Ϳ��� �� Ŭ������ ������ �� �ִ� �޴� �׸��� �߰��մϴ�. ���� �̸��� �޴� �̸�, ������ �����մϴ�.
public class NaturalNumberValidator : TMP_InputValidator// �� Ŭ������ TMP_InputValidator�� ��ӹ޾� �ڿ���(0-9)�� �Է��� �� �ֵ��� �ϴ� ��ȿ�� �˻�⸦ �����մϴ�.
{
    // �� �޼���� �Էµ� ���ڰ� �ڿ���(0-9)���� Ȯ���մϴ�.
    // ref Ű����� �ش� �Ű������� ������ ���޵��� ��Ÿ���ϴ�. ��, �޼��� ������ �� �Ű������� ���� �����ϸ� ȣ���� �������� ����� ���� �ݿ��˴ϴ�.
    // �������̵带 �ؾ� ������ Validate �޼��带 �������� �� �ֽ��ϴ�.
    // text�� pos�� �Էµ� �ؽ�Ʈ�� ���� ��ġ�� ��Ÿ���ϴ�. ������ ���������� ������ �ʽ��ϴ�.
    // ch�� �Էµ� �����Դϴ�.
    public override char Validate(ref string text, ref int pos, char ch)
    {
        if (ch >= '0' && ch <= '9')//ch�� '0'���� '9' ������ �������� Ȯ���մϴ�. ���� �׷��ٸ�, �ش� ���ڸ� ��ȯ�մϴ�.
        {
            return ch;// �Էµ� ���ڸ� �ؽ�Ʈ�� �߰��մϴ�.
        }
        return '\0'; // ���ڰ� �ƴϸ� null ���� ��ȯ (�Է� ����)
    }
}