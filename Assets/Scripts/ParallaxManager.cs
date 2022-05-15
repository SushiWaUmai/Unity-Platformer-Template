// Parallax Manager made by SushiWaUmai
// Check out the gist at https://gist.github.com/SushiWaUmai/cd5c014755f68683558642b1cf479942
// Also checkout my account on GitHub https://github.com/SushiWaUmai
using UnityEngine;

namespace Parallax
{
#if UNITY_EDITOR
    using UnityEditor;
    public class IsSortingLayerAttribute : PropertyAttribute { }
    [CustomPropertyDrawer(typeof(IsSortingLayerAttribute))]
    public class IsSortyingLayerPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int selected = -1;

            var names = new string[SortingLayer.layers.Length + 1];
            for (int i = 0; i < SortingLayer.layers.Length; i++)
            {
                names[i] = SortingLayer.layers[i].name;
                if (property.stringValue == names[i])
                    selected = i;
            }
            names[names.Length - 1] = "Add Sorting Layer...";

            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            selected = EditorGUI.Popup(position, selected, names);
            EditorGUI.EndProperty();

            if (selected == names.Length - 1)
            {
                // Open ProjectSettings/TagManager.asset in Inspector
                UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath("ProjectSettings/TagManager.asset", typeof(UnityEngine.Object));
                Selection.activeObject = obj;

                return;
            }

            if (selected >= 0)
                property.stringValue = names[selected];
        }
    }
#else
        public class IsSortingLayerAttribute : PropertyAttribute { }
#endif

    public class ParallaxManager : MonoBehaviour
    {
        [SerializeField] private Sprite[] _backgroundImages;
        [SerializeField] private Vector2Int _backgroundImagesCount = new Vector2Int(3, 1);
        [SerializeField] private Vector2 _parallaxScale = Vector2.one;
        [SerializeField] private bool _enableParallaxX = true;
        [SerializeField] private bool _enableParallaxY = false;
        [SerializeField] private float _layerOffset = 1f;
        [IsSortingLayer, SerializeField] private string _sortingLayer = "Default";
        [SerializeField] private int _sortingOrder = 0;


        private void Start()
        {
            Vector2 parallaxFactorStep = Vector2.one / _backgroundImages.Length;

            for (int i = 0; i < _backgroundImages.Length; i++)
            {
                GameObject layer = new GameObject("ParallaxLayer" + i);
                layer.transform.parent = transform;

                ParallaxLayer parallaxLayer = layer.AddComponent<ParallaxLayer>();
                Vector2 spriteSize = _backgroundImages[i].bounds.size * _parallaxScale;
                parallaxLayer.Init(
                    parallaxFactorStep * i,
                    _enableParallaxX,
                    _enableParallaxY,
                    spriteSize
                );

                Vector3 startPosition = -spriteSize * ((Vector2)_backgroundImagesCount / 2)
                    + spriteSize / 2;

                for (int x = 0; x < _backgroundImagesCount.x; x++)
                {
                    for (int y = 0; y < _backgroundImagesCount.y; y++)
                    {
                        GameObject background = new GameObject($"Background{i} ({x}, {y})");
                        background.transform.parent = layer.transform;
                        background.transform.localPosition = startPosition + new Vector3(x * spriteSize.x, y * spriteSize.y, i * _layerOffset);
                        background.transform.localScale = _parallaxScale;

                        SpriteRenderer spriteRenderer = background.AddComponent<SpriteRenderer>();
                        spriteRenderer.sprite = _backgroundImages[i];

                        spriteRenderer.sortingLayerName = _sortingLayer;
                        spriteRenderer.sortingOrder = _sortingOrder;
                    }
                }
            }
        }

        private class ParallaxLayer : MonoBehaviour
        {
            private Vector2 _parallaxFactor;
            private bool _enableParallaxX;
            private bool _enableParallaxY;
            private Vector2 _spriteSize;
            private Transform _camTransform;
            private Vector3 _startPos;

            public void Init(Vector2 parallaxFactor, bool enableX, bool enableY, Vector2 spriteSize)
            {
                _parallaxFactor = parallaxFactor;
                _enableParallaxX = enableX;
                _enableParallaxY = enableY;
                _spriteSize = spriteSize;
            }

            private void Start()
            {
                _camTransform = Camera.main.transform;
                _startPos = transform.position;
            }

            private void FixedUpdate()
            {
                Vector2 temp = Vector2.Scale(_camTransform.position, (Vector2.one - _parallaxFactor));
                Vector3 dist = _camTransform.position * _parallaxFactor;

                transform.position = _startPos + dist;

                if (_enableParallaxX)
                {
                    if (temp.x > _startPos.x + _spriteSize.x) _startPos.x += _spriteSize.x;
                    else if (temp.x < _startPos.x - _spriteSize.x) _startPos.x -= _spriteSize.x;
                }

                if (_enableParallaxY)
                {
                    if (temp.y > _startPos.y + _spriteSize.y) _startPos.y += _spriteSize.y;
                    else if (temp.y < _startPos.y - _spriteSize.y) _startPos.y -= _spriteSize.y;
                }
            }
        }
    }
}

