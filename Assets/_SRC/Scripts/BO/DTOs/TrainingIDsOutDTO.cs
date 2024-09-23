
using UnityEngine;

public class TrainingIDsOutDTO : OutDTO
{
    string[] trainingIds;

    public TrainingIDsOutDTO(string[] trainingIds)
    {
        this.trainingIds = trainingIds;
    }

    public override WWWForm ToForm()
    {
        WWWForm form = new WWWForm();
        for(int i = 0; i< trainingIds.Length; i++)
        {
            form.AddField("trainingIds[" + i + "]", trainingIds[i]);
        }
        return form;
    }
}
