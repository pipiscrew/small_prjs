/*
source
https://social.msdn.microsoft.com/Forums/windows/en-US/59a3c77e-7e9f-4e75-bb7c-1658b1e79218/datagridview-combobox-on-cell-click-in-c?forum=winformsdatacontrols

At first, we need to create our own ComboBoxColumn, ComboBoxCell and ComboBoxControl to have the ComboBox not show when the cell is not in edit mode. This is the code snippet:
*/

//Custom DataGridViewColumn
public class DataGridViewComboBoxColumn : DataGridViewColumn
{
    public DataGridViewComboBoxColumn()
        : base(new DataGridViewComboBoxCell())
    {
    }

    public override DataGridViewCell CellTemplate
    {
        get
        {
            return base.CellTemplate;
        }
        set
        {
            if ((value != null) &&
                (!value.GetType().IsAssignableFrom(typeof(DataGridViewComboBoxCell))))
            {
                throw new InvalidCastException("Must be a DataGridViewHelpCombBoxCell");
            }
            base.CellTemplate = value;
        }
    }

    public override object Clone()
    {
        DataGridViewComboBoxColumn col = base.Clone() as DataGridViewComboBoxColumn;
        //Copy the added properties, otherwise it would cause some error, such as serialization error.
        col.ValueMember = this.ValueMember;
        col.DisplayMember = this.ValueMember;
        col.DataSource = this.DataSource;
        col.ValueType = this.ValueType;

        return col;
    }

    //Data source of the combobox.
    public object DataSource { set; get; }
    //Display member of the combobox.
    public string DisplayMember { set; get; }
    //Value member of the combobox.
    public string ValueMember { set; get; }
    //Type of the value in combobox.
    public Type ValueType { set; get; }
}

public class DataGridViewComboBoxCell : DataGridViewTextBoxCell
{
    public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
    {
        base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
        ComboBox combBox = DataGridView.EditingControl as ComboBox;
        combBox.DropDownStyle = ComboBoxStyle.DropDownList;
        DataGridViewComboBoxColumn col = DataGridView.Columns[ColumnIndex] as DataGridViewComboBoxColumn;
        //Transfer values from column to combobox.
        combBox.DataSource = col.DataSource;
        combBox.DisplayMember = col.DisplayMember;
        combBox.ValueMember = col.ValueMember;
        try
        {
            object value = this.GetValue(rowIndex);
            //If value is null, set the value to the first value in data source.
            if (value != null)
                combBox.SelectedValue = value;
            else
                combBox.SelectedIndex = 0;
        }
        catch
        {
            MessageBox.Show("Set value fail.");
        }
    }

    public override Type ValueType
    {
        get
        {
            return (DataGridView.Columns[ColumnIndex] as DataGridViewComboBoxColumn).ValueType;
        }
    }

    public override Type FormattedValueType
    {
        get
        {
            return typeof(string);
        }
    }

    public override object DefaultNewRowValue
    {
        get
        {
            return null;
        }
    }

    public override Type EditType
    {
        get
        {
            return typeof(ComboBoxControl);
        }
    }

    public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
    {
        //Parse null value to blank.
        if (formattedValue == null)
            return "";
        else
            return formattedValue.ToString();
    }

    protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    {
        //When the value is null, draw blank.
        string newVal = "";
        if (this.DataGridView.Rows[rowIndex].IsNewRow == false)
        {
            if ((value != null) && (value.ToString().Trim() != ""))
            {
                newVal = value.ToString().Trim();
            }
        }

        base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, value, errorText, cellStyle, advancedBorderStyle, paintParts);
    }   
}

public class ComboBoxControl : ComboBox, IDataGridViewEditingControl
{
    private int index_ = 0;
    private DataGridView dataGridView_ = null;
    private bool valueChanged_ = false;

    public ComboBoxControl()
        : base()
    {
        this.SelectedIndexChanged += new EventHandler(HelpCombBoxControl_SelectedIndexChanged);
    }

    void HelpCombBoxControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        NotifyDataGridViewOfValueChange();
    }

    protected virtual void NotifyDataGridViewOfValueChange()
    {
        this.valueChanged_ = true;
        if (this.dataGridView_ != null)
        {
            this.dataGridView_.NotifyCurrentCellDirty(true);
        }
    }

    #region IDataGridViewEditingControl members

    public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
    {
        this.Font = dataGridViewCellStyle.Font;
        this.ForeColor = dataGridViewCellStyle.ForeColor;
        this.BackColor = dataGridViewCellStyle.BackColor;
    }

    public DataGridView EditingControlDataGridView
    {
        get
        {
            return dataGridView_;
        }
        set
        {
            dataGridView_ = value;
        }
    }

    public object EditingControlFormattedValue
    {
        get
        {
            return base.SelectedValue;
        }
        set
        {
            base.SelectedValue = value;
        }
    }

    public int EditingControlRowIndex
    {
        get
        {
            return index_;
        }
        set
        {
            index_ = value;
        }
    }

    public bool EditingControlValueChanged
    {
        get
        {
            return valueChanged_;
        }
        set
        {
            valueChanged_ = value;
        }
    }

    public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
    {
        if (keyData == Keys.Return)
            return true;
        switch (keyData & Keys.KeyCode)
        {
            case Keys.Left:
            case Keys.Up:
            case Keys.Down:
            case Keys.Right:
            case Keys.Home:
            case Keys.End:
            case Keys.PageDown:
            case Keys.PageUp:
            case Keys.Return:
                return true;
            default:
                return false;
        }
    }

    public Cursor EditingPanelCursor
    {
        get
        {
            return base.Cursor;
        }
    }

    public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
    {
        return EditingControlFormattedValue;
    }

    public void PrepareEditingControlForEdit(bool selectAll)
    {
    }

    public bool RepositionEditingControlOnValueChange
    {
        get { return false; }
    }
    #endregion

}


 /*

Second, we need to set the EditMode of the DataGridView to EditProgrammatically as follows:
this.dataGridView2.EditMode = DataGridViewEditMode.EditProgrammatically;

Then handle the CellDoubleClick event to initial the edit mode:
*/

private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
{
    //Show the combo box.

    this.dataGridView2.BeginEdit(false);
}

 
/*
Let me know if this helps.
Aland Li