using Inputs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InputsText : MonoBehaviour
{
    [SerializeField] private Transform pointer;
    [SerializeField] private float AxisZ = 0;
    [SerializeField] private Text pole;
    private Vector3 vector3;
    private IInputPlayerExecutor inputs;
    [Inject]
    public void Init(IInputPlayerExecutor _inputs)
    {
        inputs = _inputs;
    }
    private void OnEnable()
    {
        inputs.Enable();
        inputs.OnMousePoint += MousePoint;
        inputs.OnStartPressMouse += StartPressMouse;

        inputs.OnMoveButton += MoveButton;
        inputs.OnStartPressButton += StartPress;
        inputs.OnCancelPressButton += CancelPress;
    }
    private void MoveButton(InputButtonData data)
    {
        pole.text = $"Кнопки WASD {data.WASD}";
    }
    private void MousePoint(Vector2 hit)
    {
        vector3 = hit;
        vector3.z = AxisZ;
    }
    private void StartPressMouse(InputMouseData _inputMouseData)
    {
        pointer.position = vector3;
        pole.text = $"Мыш {_inputMouseData.MouseLeftButton} {_inputMouseData.MouseRightButton} {_inputMouseData.MouseMiddleButton}  " +
            $"position = {vector3}";
    }
    private void StartPress(InputButtonData data)
    {
        pole.text = $"нажать Esc/Space {data.Esc} {data.Space}";
    }
    private void CancelPress(InputButtonData data)
    {
        pole.text = $"отжать Esc/Space {data.Esc} {data.Space}";
    }
    void Start()
    {

    }
}
