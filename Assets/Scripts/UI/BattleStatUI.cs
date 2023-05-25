using System;
using Actor.Player;
using Actor.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BattleStatUI : MonoBehaviour
    {
        private Player _player;
        
        [SerializeField] private Slider healthSlider;
    
        [SerializeField] private TMP_Text atkText;
        [SerializeField] private TMP_Text agiText;
        [SerializeField] private TMP_Text defText;

        private void Awake()
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
            OnAttributeChanged();
            OnHPChanged();
        }

        private void OnEnable()
        {
            _player.OnAttributeChanged += OnAttributeChanged;
            _player.OnHPChanged += OnHPChanged;
        }

        private void OnDisable()
        {
            _player.OnAttributeChanged -= OnAttributeChanged;
            _player.OnHPChanged -= OnHPChanged;
        }

        private void OnAttributeChanged()
        {
            atkText.text = "ATK : " + _player.GetAttributeValue(AttributeType.Attack);
            agiText.text = "AGI : " + _player.GetAttributeValue(AttributeType.Speed);
            defText.text = "DEF : " + _player.GetAttributeValue(AttributeType.Defense);
        }

        private void OnHPChanged()
        {
            healthSlider.value = _player.PercentHealPoint;
        }
    }
}