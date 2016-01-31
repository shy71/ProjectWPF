<Window
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:PLForms" x:Class="PLForms.ClientInterface"
        Title="ClientInterface" Height="1000" Width="1500" Loaded="Window_Loaded" Background="#FFA50221" PreviewKeyDown="KeyDownCheck" PreviewKeyUp="KeyUpCheck">
    <Grid x:Name="BigGrid">
        <Grid.RowDefinitions>
            <RowDefinition Height="417*"/>
            <RowDefinition Height="68*"/>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="1*"/>
            <ColumnDefinition Width="4*"/>
        </Grid.ColumnDefinitions>
        <Button x:Name="LogOut" Content="Log Out" HorizontalAlignment="Right" VerticalAlignment="Top" Grid.Column="1" FontSize="24" Click="LogOut_Click"/>
        <Image x:Name="add" Source="Images/Add.png" Height="150" Width="150" PreviewMouseDown="BigGrid_MouseDown"/>
        <ScrollViewer Grid.Column="1"  VerticalScrollBarVisibility="Auto" Grid.RowSpan="1">
            <StackPanel>
                <Label x:Name="MainTitle" HorizontalContentAlignment="Center" FontSize="50"/>
                <StackPanel x:Name="stackPanel">
                    <Label x:Name="LittleTitle" Content="Active Orders" HorizontalContentAlignment="Center" FontSize="40"/>
                </StackPanel>
            </StackPanel>
        </ScrollViewer>
        <StackPanel x:Name="MenuStack" Grid.RowSpan="2">
            <Label Content="Options Menu:" FontFamily="Comic Sans MS" FontSize="30" FontWeight="Bold" />
            <RadioButton x:Name="UnsentButton" Content="Unsent Orders" VerticalContentAlignment="Center" HorizontalContentAlignment="Center" FontFamily="Comic Sans MS" FontSize="30" HorizontalAlignment="Center" VerticalAlignment="Center" Checked="UnsentButton_Checked"/>
            <!--<Label  Grid.Row="0"  MouseLeftButtonUp="Unsent_MouseLeftButtonUp"/>-->
            <Expander x:Name="UnsentExpender"  MaxHeight="500" Header="Unsent Orders List" HorizontalAlignment="Right"  VerticalAlignment="Bottom" IsExpanded="False" ExpandDirection="Down" Expanded="Expender_Expanded" Width="220">
                <ScrollViewer  VerticalScrollBarVisibility="Auto">
                    <StackPanel x:Name="UnsentStack">
                    </StackPanel>
                </ScrollViewer>
            </Expander>
            <RadioButton x:Name="ActiveButton" Content="Active Orders" VerticalContentAlignment="Center" HorizontalContentAlignment="Center" FontFamily="Comic Sans MS" FontSize="30" HorizontalAlignment="Center" VerticalAlignment="Center" Checked="ActiveButton_Checked"/>
            <Expander x:Name="ActiveExpender"  Header="Active Orders List" HorizontalAlignment="Right"  VerticalAlignment="Bottom" IsExpanded="False" ExpandDirection="Down" Expanded="Expender_Expanded" Width="220">
                <ScrollViewer  VerticalScrollBarVisibility="Auto">
                    <StackPanel x:Name="ActiveStack">
                    </StackPanel>
                </ScrollViewer>
            </Expander>
            <RadioButton x:Name="DeliveredButton" Content="Delivered Orders" VerticalContentAlignment="Center" HorizontalContentAlignment="Center" FontFamily="Comic Sans MS" FontSize="30" HorizontalAlignment="Right" VerticalAlignment="Center"  Checked="DeliveredButton_Checked"/>
            <Expander x:Name="DeliveredExpender"   Header="Active Orders List" HorizontalAlignment="Right"  VerticalAlignment="Bottom" IsExpanded="False" ExpandDirection="Down"  Expanded="Expender_Expanded" Width="220">
                <ScrollViewer  VerticalScrollBarVisibility="Auto">
                    <StackPanel x:Name="DeliveredStack">
                    </StackPanel>
                </ScrollViewer>
            </Expander>


        </StackPanel>
        <Button Content="Edit your profile" HorizontalAlignment="Center" VerticalAlignment="Bottom" Width="150" Click="Button_Click" Margin="74,0" Grid.Row="1"/>
        <StackPanel Orientation="Horizontal" Grid.Column="1" Grid.Row="1" HorizontalAlignment="Right">
            <Image x:Name="DeleteImg" Source="Images\delete-b.jpg" Margin="0,0,30,0" PreviewMouseDown="Delete_PreviewMouseDown" Visibility="Collapsed" ToolTip="Delete The Orders"/>
            <Image x:Name="EditImg" Source="Images\Edit.png" Margin="0,0,30,0" PreviewMouseDown="Edit_PreviewMouseDown" Visibility="Collapsed"/>
            <Image x:Name="SendImg" Source="Images\Send2.jpg" Margin="0,0,30,0" PreviewMouseDown="Send_PreviewMouseDown" Visibility="Collapsed" ToolTip="Send The Orders"/>
            <Image x:Name="ArivedImg" Source="Images\data_arrived1.png" Margin="0,0,30,0" PreviewMouseDown="Arived_PreviewMouseDown" Visibility="Collapsed" ToolTip="Report The Orders as arived"/>

        </StackPanel>
        
        <!--<Grid  Grid.Column="0">
            <Grid.RowDefinitions>
                <RowDefinition Height="1*"/>
                <RowDefinition Height="1*"/>
                <RowDefinition Height="1*"/>
                <RowDefinition Height="1*"/>
                <RowDefinition Height="1*"/>
                <RowDefinition Height="1*"/>
                <RowDefinition Height="1*"/>
                <RowDefinition Height="1*"/>
                <RowDefinition Height="1*"/>
                <RowDefinition Height="1*"/>
                <RowDefinition Height="1*"/>
            </Grid.RowDefinitions>
            <RadioButton x:Name="UnsentButton" Grid.Row="0" HorizontalContentAlignment="Center" HorizontalAlignment="Center" VerticalAlignment="Center" Margin="27,38,259,38" Checked="UnsentButton_Checked" Panel.ZIndex="1"/>
            <Label Content="Unsent Orders" Grid.Row="0" VerticalContentAlignment="Center" HorizontalContentAlignment="Center" FontFamily="Comic Sans MS" FontSize="30" MouseLeftButtonUp="Unsent_MouseLeftButtonUp"/>
            <Expander Grid.Row="0"  Header="Unsent Orders List" HorizontalAlignment="Center"  VerticalAlignment="Bottom" IsExpanded="False" ExpandDirection="Down" >
                <Grid Background="#FFE5E5E5">
                    <TextBox Text="SHy"/>
                </Grid>
            </Expander>
            <RadioButton x:Name="ActiveButton" Grid.Row="1" HorizontalContentAlignment="Center" HorizontalAlignment="Center" VerticalAlignment="Center"  Checked="ActiveButton_Checked" Panel.ZIndex="1" Margin="27,38,259,38"/>
            <Label  Content="Active Orders" Grid.Row="1" VerticalContentAlignment="Center" HorizontalContentAlignment="Center" FontFamily="Comic Sans MS" FontSize="30" MouseLeftButtonUp="Active_MouseLeftButtonUp"/>
            <RadioButton x:Name="DeliveredButton" Grid.Row="2" HorizontalAlignment="Center" VerticalAlignment="Center"  Checked="DeliveredButton_Checked" Panel.ZIndex="1" Margin="27,38,259,38" />
            <Label Content="Delivered Orders" Grid.Row="2" VerticalContentAlignment="Center" HorizontalContentAlignment="Right" FontFamily="Comic Sans MS" FontSize="30" MouseLeftButtonUp="Deliverd_MouseLeftButtonUp"/>
            <Button Content="Edit your profile" HorizontalAlignment="Center" Grid.Row="10" VerticalAlignment="Center" Width="150" Click="Button_Click"/>


        </Grid>-->

    </Grid>
</Window>
