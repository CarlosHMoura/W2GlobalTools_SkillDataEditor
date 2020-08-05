using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using W2___SkillData_Editor.Funções;

namespace W2___SkillData_Editor
{
    public partial class Window : Form
    {
        public string FilePatch = "";
        public Window()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox1.Text = "Selecione...";
            comboBox2.Text = "Selecione...";
            comboBox3.Text = "Selecione...";
            comboBox4.Text = "Selecione...";
            for (int i = 0; i < 5; i++)
            {
                comboBox1.Items.Add("(" + i + ") " + External.Propriendades[i]);
                comboBox2.Items.Add("(" + i + ") " + External.Propriendades[i]);
            }
            for (int i = 0; i < 51; i++)
            {
                comboBox3.Items.Add("(" + i + ") " + External.EffectsString[i].Replace('_', ' '));
                comboBox4.Items.Add("(" + i + ") " + External.EffectsString[i].Replace('_', ' '));
            }
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "http://www.webcheats.com.br/members/seitbnao.4781487/";
            linkLabel1.Links.Add(link);
        }

        public void _Update()
        {

            if (SkillList.Items.Count > 0)
                SkillList.Items.Clear();


            for (int i = 0; i < 248; i++)
            {
                if (i < 108)
                    SkillList.Items.Add("(" + i.ToString().PadLeft(4, '0') + ") " + External.SkillName[i].Replace('_', ' '));
                else if (i >= 200)
                    SkillList.Items.Add("(" + i.ToString().PadLeft(4, '0') + ") " + External.nSkillname[i - 200].Replace('_', ' '));
                else
                    SkillList.Items.Add(" - ");
            }

        }

        private void Carregar_Click(object sender, EventArgs e)
        {
            this.LoadSkillData();
        }

        private void Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (SkillList.Items.Count > 0 && External.Index != -1)
                        GetValue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SkillList_SelectedIndexChanged(object sender, EventArgs e)
        {
            External.Index = SkillList.SelectedIndex;
            SetValues();
        }

        public void SetValues()
        {
            int SkillID = External.Index;
            if (SkillID == -1)
                return;

            label17.Text = "" + SkillID;

           // textBox7.Text = External.g_pSkillData.Skill[SkillID].InstanceAttribute.ToString();
            comboBox2.SelectedIndex = External.g_pSkillData.Skill[SkillID].AffectResist;
            comboBox3.SelectedIndex = External.g_pSkillData.Skill[SkillID].AffectType;
            comboBox4.SelectedIndex = External.g_pSkillData.Skill[SkillID].TickType;
            checkBox3.Checked = External.g_pSkillData.Skill[SkillID].UseOnMacro == 1;
            checkBox4.Checked = External.g_pSkillData.Skill[SkillID].PartyCheck == 1;
            checkBox1.Checked = External.g_pSkillData.Skill[SkillID].Passive == 1;
            checkBox2.Checked = External.g_pSkillData.Skill[SkillID].Aggressive == 1;
            textBox2.Text = "" + External.g_pSkillData.Skill[SkillID].AffectTime;
            textBox3.Text = "" + External.g_pSkillData.Skill[SkillID].AffectValue;
            textBox4.Text = "" + External.g_pSkillData.Skill[SkillID].TickValue;
            textBox7.Text = "" + External.g_pSkillData.Skill[SkillID].InstanceAttribute;
            textBox5.Text = "" + External.g_pSkillData.Skill[SkillID].MaxTarget;
            textBox8.Text = "" + External.g_pSkillData.Skill[SkillID].SkillPoint;
            textBox9.Text = "" + External.g_pSkillData.Skill[SkillID].Delay;
            textBox6.Text = "" + External.g_pSkillData.Skill[SkillID].Range;
            textBox1.Text = "" + External.g_pSkillData.Skill[SkillID].ManaSpent;
            textBox10.Text = "";
            textBox11.Text = "";
            for (int i = 0; i < 8; i++)
            {
                textBox10.Text += External.g_pSkillData.Skill[SkillID].Act1[i] + " ";
                textBox11.Text += External.g_pSkillData.Skill[SkillID].Act2[i] + " ";
            }
            textBox12.Text = "" + External.g_pSkillData.Skill[SkillID].TargetType;
            textBox13.Text = "" + External.g_pSkillData.Skill[SkillID].InstanceType;
            textBox14.Text = "" + External.g_pSkillData.Skill[SkillID].InstanceValue;
        }
        public void GetValue()
        {
            int SkillID = External.Index;
            if (SkillID == -1)
                return;


            External.g_pSkillData.Skill[SkillID].UseOnMacro = (short)(checkBox3.Checked == false ? 0 : 1);
            External.g_pSkillData.Skill[SkillID].PartyCheck = checkBox4.Checked == false ? 0 : 1;
            External.g_pSkillData.Skill[SkillID].Passive = checkBox1.Checked == false ? 0 : 1;
            External.g_pSkillData.Skill[SkillID].Aggressive = checkBox2.Checked == false ? 0 : 1;
            External.g_pSkillData.Skill[SkillID].AffectTime = int.Parse(textBox2.Text);
            External.g_pSkillData.Skill[SkillID].AffectValue = int.Parse(textBox3.Text);
            External.g_pSkillData.Skill[SkillID].TickValue = int.Parse(textBox4.Text);
            External.g_pSkillData.Skill[SkillID].InstanceAttribute = int.Parse(textBox7.Text);
            External.g_pSkillData.Skill[SkillID].MaxTarget = int.Parse(textBox5.Text);
            External.g_pSkillData.Skill[SkillID].SkillPoint = int.Parse(textBox8.Text);
            External.g_pSkillData.Skill[SkillID].Delay = int.Parse(textBox9.Text);
            External.g_pSkillData.Skill[SkillID].Range = int.Parse(textBox6.Text);
            External.g_pSkillData.Skill[SkillID].ManaSpent = int.Parse(textBox1.Text);

            string[] Texter_1 = textBox10.Text.Split(new char[] { ' ' });
            string[] Texter_2 = textBox11.Text.Split(new char[] { ' ' });
            for (int i = 0; i < 8; i++)
            {

                External.g_pSkillData.Skill[SkillID].Act1[i] = sbyte.Parse(Texter_1[i]);
                External.g_pSkillData.Skill[SkillID].Act2[i] = sbyte.Parse(Texter_2[i]);
            }
            External.g_pSkillData.Skill[SkillID].TargetType = int.Parse(textBox12.Text);
            External.g_pSkillData.Skill[SkillID].InstanceType = int.Parse(textBox13.Text);
            External.g_pSkillData.Skill[SkillID].InstanceValue = int.Parse(textBox14.Text);
           // External.g_pSkillData.Skill[SkillID].InstanceAttribute = comboBox1.SelectedIndex;
            External.g_pSkillData.Skill[SkillID].AffectResist = comboBox2.SelectedIndex;
            External.g_pSkillData.Skill[SkillID].AffectType = comboBox3.SelectedIndex;
            External.g_pSkillData.Skill[SkillID].TickType = comboBox4.SelectedIndex;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SkillID = External.Index;
            if (SkillID == -1 || comboBox1.SelectedIndex == -1)
                return;
            External.g_pSkillData.Skill[SkillID].InstanceAttribute = comboBox1.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SkillID = External.Index;
            if (SkillID == -1 || comboBox2.SelectedIndex == -1)
                return;
            External.g_pSkillData.Skill[SkillID].AffectResist = comboBox2.SelectedIndex;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SkillID = External.Index;
            if (SkillID == -1 || comboBox3.SelectedIndex == -1)
                return;
            External.g_pSkillData.Skill[SkillID].AffectType = comboBox3.SelectedIndex;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SkillID = External.Index;
            if (SkillID == -1 || comboBox4.SelectedIndex == -1)
                return;
            External.g_pSkillData.Skill[SkillID].TickType = comboBox4.SelectedIndex;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //External.Version = 7556;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //External.Version = 7559;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
        public void LoadSkillData()
        {
            try
            {

                using (OpenFileDialog find = new OpenFileDialog())
                {
                    find.Filter = "SkillData.bin|SkillData.bin";
                    find.Title = "Selecione sua SkillData.bin";

                    find.ShowDialog();

                    if (find.CheckFileExists)
                    {
                        this.FilePatch = find.FileName;

                        if (File.Exists(this.FilePatch))
                        {
                            byte[] temp = File.ReadAllBytes(this.FilePatch);

                            if (temp.Length != 23812 && temp.Length != 23808)
                            {
                                MessageBox.Show("SkillData inválida", "SkillData.bin inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.LoadSkillData();
                            }
                            else
                            {

                                if (Functions.ReadSkillData(this.FilePatch))
                                {
                                    salvarSkillDatabinToolStripMenuItem.Enabled = true;
                                    salvarSkillDatacsvToolStripMenuItem.Enabled = true;
                                    Salvar.Enabled = true;
                                    colocar99NoTimeAllSkillsToolStripMenuItem.Enabled = true;
                                    this._Update();
                                }
                       
                            }
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
            }
            catch (Exception ex)
            {
                salvarSkillDatabinToolStripMenuItem.Enabled = false;
                salvarSkillDatacsvToolStripMenuItem.Enabled = false;
                Salvar.Enabled = false;
                colocar99NoTimeAllSkillsToolStripMenuItem.Enabled = false;
                MessageBox.Show(ex.Message);
            }
        }
        private void abrirSkillDatabinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadSkillData();
        }
        public void SaveSkillDataCSV()
        {
            try
            {
                int SkillID = External.Index;
                if (SkillID == -1 || comboBox4.SelectedIndex == -1)
                    return;

                using (SaveFileDialog save = new SaveFileDialog())
                {
                    save.Filter = "*.csv|*.csv";
                    save.Title = "Selecione onde deseja salvar sua SkillData.csv";
                    save.ShowDialog();

                    if (save.FileName != "")
                    {

                        List<string> Itens = new List<string>();
                        ComboBox Combo = new ComboBox();

                        StreamWriter File = new StreamWriter(save.FileName);

                        string TxT = "";


                        File.WriteLine("#by Seitbnao\n #SkillID,SkillPoint,TargetType,ManaSpent,Delay,Range,InstanceType,InstanceValue,TicktType,TicketValue,AffectType,AffectValue,AffectTime,Act[8],Act2[8],InstanceAtribute,TickAttribute,Agressive,Maxtarget,PatyCheck,Resist,PassiveCheck,MacroValue,SkillName\n");
                        for (int i = 0; i < 248; i++)
                        {
                            TxT = "";
                            string act1 = "", act2 = "", skillname = "";
                            for (int x = 0; x < 8; x++)
                            {
                                act1 += "." + External.g_pSkillData.Skill[i].Act1[x];
                                act2 += "." + External.g_pSkillData.Skill[i].Act2[x];
                            }
                            if (i < 108)
                                skillname = External.SkillName[i];
                            else if (i >= 200)
                                skillname = External.nSkillname[i - 200];
                            else
                                continue;


                            TxT += i + "," + External.g_pSkillData.Skill[i].SkillPoint
                            + "," + External.g_pSkillData.Skill[i].TargetType
                            + "," + External.g_pSkillData.Skill[i].ManaSpent
                            + "," + External.g_pSkillData.Skill[i].Delay
                            + "," + External.g_pSkillData.Skill[i].Range
                            + "," + External.g_pSkillData.Skill[i].InstanceType
                            + "," + External.g_pSkillData.Skill[i].InstanceValue
                            + "," + External.g_pSkillData.Skill[i].TickType
                            + "," + External.g_pSkillData.Skill[i].TickValue
                            + "," + External.g_pSkillData.Skill[i].AffectType
                            + "," + External.g_pSkillData.Skill[i].AffectValue
                            + "," + External.g_pSkillData.Skill[i].AffectTime
                            + "," + act1
                            + "," + act2
                             + "," + External.g_pSkillData.Skill[i].InstanceAttribute
                             + "," + External.g_pSkillData.Skill[i].TickAttribute
                             + "," + External.g_pSkillData.Skill[i].Aggressive
                             + "," + External.g_pSkillData.Skill[i].MaxTarget
                             + "," + External.g_pSkillData.Skill[i].PartyCheck
                             + "," + External.g_pSkillData.Skill[i].AffectResist
                              + "," + External.g_pSkillData.Skill[i].Passive
                               + "," + External.g_pSkillData.Skill[i].UseOnMacro
                               + "," + skillname + "";

                            if (String.IsNullOrEmpty(skillname)) continue;
                            File.WriteLine(TxT.Replace(",.", ","));
                        }
                        File.Close();









                        MessageBox.Show($"Arquivo {save.FileName} salvo com sucesso!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void salvarSkillDatacsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveSkillDataCSV();
        }

        private void salvarSkillDatabinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog save = new SaveFileDialog())
                {
                    save.Filter = "*.bin|*.bin";
                    save.Title = "Selecione onde deseja salvar sua SkillData.bin";
                    save.ShowDialog();

                    if (save.FileName != "")
                    {
                        byte[] toSave = Pak.ToByteArray(External.g_pSkillData);
                        byte[] pKeyList = File.ReadAllBytes("./Keys.bin");
                        Array.Resize(ref pKeyList, pKeyList.Length + 1);

                        
                       
                            for (int i = 0; i < toSave.Length; i++)
                            toSave[i] ^= (pKeyList[i & 63]);
                       

                        File.Create(save.FileName).Close();
                        File.WriteAllBytes(save.FileName, toSave);

                        MessageBox.Show($"Arquivo {save.FileName} salvo com sucesso!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Pesquisa_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < SkillList.Items.Count; i++)
            {
                if (SkillList.Items[i].ToString().ToLower().Contains(Pesquisa.Text.ToLower()))
                {
                    SkillList.SetSelected(i, true);
                    break;
                }
            }
        }

        private void colocar99NoTimeAllSkillsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 248; i++)
                External.g_pSkillData.Skill[i].AffectTime = 99;

            MessageBox.Show("Tempo de todos os Buffs alterados para 99 Segundos");
        }
    }
}