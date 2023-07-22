using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using YG;

public class DynamicResolution : MonoBehaviour
{
    UniversalRenderPipelineAsset urp;
    [SerializeField] float maxDPI = 0.95f; // �� ���������� � ������� �� ����� ��� 1
    [SerializeField] float minDPI = 0.55f;
    [SerializeField] float dampen = 0.02f; // �� ����� ���-�� �� ��������\��������� ����������

    // ���� ��� ����� ���� - ���������� �������� �� �����.
    [SerializeField] float maxFps = 75;
    [SerializeField] float minFps = 55;

    [SerializeField] float renderScale = 1f;
    [SerializeField] float refreshResolutionTime = 1f;


    private float timer = 0; // ��� Update'�
    private float deltaTime; // ��� GetFps


    void Start()
    {
            urp = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
            // ��������� RenderScale ������ �����������.
            // ������� �� ��������� ���������� �� �� ���������
            urp.renderScale = renderScale;
    }
    void Update()
    {
        if (!YandexGame.EnvironmentData.isDesktop)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                ResolutionUpdate();
                timer = refreshResolutionTime;
            }
        }
    }
    void ResolutionUpdate()
    {
        float fps = GetFps();
        if (maxFps < fps)
        {
            ImproveResolution();
        }

        if (minFps > fps)
        {
            SubtructResolution();
        }

        renderScale = urp.renderScale;
    }
    void ImproveResolution()
    {
        if (renderScale < maxDPI)
        {
            urp.renderScale += dampen;
        }
    }
    void SubtructResolution()
    {
        if (renderScale > minDPI)
        {
            urp.renderScale -= dampen;
        }
    }

    float GetFps()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        return fps;
    }
}
