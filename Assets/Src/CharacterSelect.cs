using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public Image selectedFrame; // viền khung chọn
    public static CharacterSelect currentlySelected; // lưu nhân vật đang được chọn

    // Khi click nút
    public void OnSelect()
    {
        // Nếu đã có nhân vật khác được chọn -> tắt khung cũ
        if (currentlySelected != null)
            currentlySelected.selectedFrame.enabled = false;

        // Lưu nhân vật mới
        currentlySelected = this;

        // Bật khung viền cho nhân vật này
        selectedFrame.enabled = true;

        // (Tuỳ chọn) Lưu nhân vật được chọn vào PlayerPrefs
        PlayerPrefs.SetString("SelectedCharacter", gameObject.name);
    }
}
