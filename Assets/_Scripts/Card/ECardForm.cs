using System.Collections.Generic;

public enum ECardForm
{
    NoForm = 0,

    Type1 = 1,
    Type2 = 2,
    Type3 = 3,
    Type4 = 4,
    Type5 = 5,
    Type6 = 6,
}

public static class CardFormCondition
{
    private static List<ECardForm> validOrder = new List<ECardForm>
    {
        ECardForm.Type1,
        ECardForm.Type3,
        ECardForm.Type2,
        ECardForm.Type6,
        ECardForm.Type5,
        ECardForm.Type4
    };

    public static bool IsValidFollow(ECardForm currentForm, ECardForm newForm)
    {
        int index = validOrder.IndexOf(currentForm);
        if (index == -1) return false;

        int nextIndex = (index + 1) % validOrder.Count;
        return validOrder[nextIndex] == newForm;
    }
}