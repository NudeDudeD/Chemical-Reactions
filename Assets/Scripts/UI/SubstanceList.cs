using UnityEngine;

public class SubstanceList : MonoBehaviour
{
    [SerializeField] private IconFrame _panelPrefab;
    [SerializeField] private PanelSorter _sorter;

    private void Start()
    {
        foreach (Pair<Substance, MaterialSettings> pair in DataStorage.SubstancesInformation)
        {
            IconFrame cell = Instantiate(_panelPrefab);
            cell.Redraw(pair.Key.Name, IconStorage.GetTexture(pair.Key));

            RectTransform rectTransform = cell.GetComponent<RectTransform>();
            _sorter.AddPanel(rectTransform);
        }
    }
}