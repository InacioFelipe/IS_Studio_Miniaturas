<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:frmwrk="Corel Framework Data">
  <xsl:output method="xml" encoding="UTF-8" indent="yes"/>

  <frmwrk:uiconfig>
   
    <frmwrk:applicationInfo userConfiguration="true" />
  </frmwrk:uiconfig>

  <!-- Copy everything -->
  <xsl:template match="node()|@*">
    <xsl:copy>
      <xsl:apply-templates select="node()|@*"/>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="uiConfig/items">
    <xsl:copy>
      <xsl:apply-templates select="node()|@*"/>
		<!-- Defina o botão que conterá um menu igual em todos os projetos. -->
		<!-- TituloDoBotaoParaOsProjetos -->
		<itemData guid="f1d3d1d0-cc8d-4f04-91cb-7112255b8af1" noBmpOnMenu="true"
				  type="flyout"
				  dynamicCategory="2cc24a3e-fe24-4708-9a74-9c75406eebcd"
				  userCaption="IS Studio"
				  enable="true"
				  flyoutBarRef="FB727225-CEA7-4D27-BB27-52C687B53029"
                />
		<!-- Defina o botão que exibe o Docker. -->
		<!-- GUID A -->
		<!-- GUID C -->
		<!-- NomeDaDocker -->
		<itemData guid="B29B4279-261F-41A3-83BC-56364E5CAF9C" noBmpOnMenu="true"
            type="checkButton"
            check="*Docker('837D02AB-8D29-4F2D-A9AF-C00B2B3D56CD')"
            dynamicCategory="2cc24a3e-fe24-4708-9a74-9c75406eebcd"
            userCaption="IS Miniaturas"
            enable="true"/>

      <!-- Defina o controle web que será colocado na Docker -->
	  <!-- GUID B -->
      <itemData guid="C066512C-DF52-4510-875B-D2F4173DE5DF"
                type="wpfhost"
                hostedType="Addons\IS_Studio_Miniaturas\IS_Studio_Miniaturas.dll,IS_Studio_Miniaturas.DockerUI"
                enable="true"/>

    </xsl:copy>
  </xsl:template>
	<!-- Defina o nome do menu para todos os projetos-->
	<!-- GUID C -->
	<!-- NomeDosProjetos -->
	<xsl:template match="uiConfig/commandBars">
		<xsl:copy>
			<xsl:apply-templates select="node()|@*"/>

			<commandBarData guid="837D02AB-8D29-4F2D-A9AF-C00B2B3D56CD"
							type="menu"
							nonLocalizableName="IS Studio"
							flyout="true">
				<menu>
					<!--Here change to new item-->
					<!--<item guidRef="DF67BEBE-6551-4F3B-BE5B-1BF46E16AB67"/>-->
				</menu>
			</commandBarData>
		</xsl:copy>
	</xsl:template>
	<xsl:template match="uiConfig/commandBars/commandBarData[guid='FB727225-CEA7-4D27-BB27-52C687B53029']/menu">
		<xsl:copy>
			<xsl:apply-templates select="node()|@*"/>

					<!--Here change to new item-->
					<item guidRef="5a2f0cfc-0388-46c7-861a-94d7b58ad00c"/>

		</xsl:copy>
	</xsl:template>
  <xsl:template match="uiConfig/dockers">
    <xsl:copy>
      <xsl:apply-templates select="node()|@*"/>

		<!-- Defina o Docker web -->
		<!-- GUID C -->
		<!-- TituloDaDocker -->
		<dockerData guid="837D02AB-8D29-4F2D-A9AF-C00B2B3D56CD"
                  userCaption="Miniaturas"
                  wantReturn="true"
                  focusStyle="noThrow">
        <container>
          <!-- Adicione o controle da página web ao Docker. -->
		  <!-- GUID B -->
          <item dock="fill" margin="0,0,0,0" guidRef="C066512C-DF52-4510-875B-D2F4173DE5DF"/>
        </container>
      </dockerData>
    </xsl:copy>
  </xsl:template>

</xsl:stylesheet>
