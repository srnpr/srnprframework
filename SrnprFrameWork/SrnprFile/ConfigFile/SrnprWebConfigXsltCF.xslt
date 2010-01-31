<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet xmlns="http://www.w3.org/1999/xhtml" version="1.0"  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
				xmlns:swc="http://SrnprFrameWork/SrnprFile/ConfigFile/SrnprWebConfig"
>
	<xsl:template match="/">
		<html>
			<title>SrnprWeb控件专用配置文件</title>
			<link href="{swc:srnprWebConfig/swc:commonConfig/swc:dllXsltCssHref}" rel="Stylesheet"/>

			<body class="SWCbody">
				<xsl:for-each select="swc:srnprWebConfig">
					<div>
						<div class="SWCListTitle">通用配置系列</div>
						<table class="SWCListTable" cellpadding="0" cellspacing="1" border="0">
							<tr>
								
								<th>字段</th>
								<th>值</th>
								<th>描述</th>
							</tr>
							<xsl:call-template name="_temp_CommonConfig"></xsl:call-template>
						</table>
						<div class="SWCListTitle">包含文件系列</div>
						<table class="SWCListTable" cellpadding="0" cellspacing="1" border="0">
							<tr>
								<th>文件编号</th>
								<th>文件链接</th>
								<th>描述</th>
							</tr>
							<xsl:call-template name="_temp_IncludeFileList"></xsl:call-template>
						</table>


						<div class="SWCListTitle">控件配置系列</div>

						<xsl:call-template name="_temp_widget"></xsl:call-template>

					</div>

				</xsl:for-each>
			</body>

		</html>
	</xsl:template>


	<xsl:template name="_temp_CommonConfig">
		<xsl:for-each select="swc:commonConfig">

			<!--
			<tr>
				<td class="SWCListTableField">
					
				</td>
				<td class="SWCListTableValue">
					<xsl:value-of select=""/>
				</td>
				<td class="SWCListTabledescription">
					
				</td>
			</tr>
			
			-->

			<tr>
				<td class="SWCListTableField">
					当前版本号：
				</td>
				<td class="SWCListTableValue">
					<xsl:value-of select="swc:dllVersion"/>
				</td>
				<td class="SWCListTabledescription">

				</td>
			</tr>
			<tr>
				<td class="SWCListTableField">
					xslt样式表：
				</td>
				<td class="SWCListTableValue">
					<xsl:value-of select="swc:dllXsltCssHref"/>
				</td>
				<td class="SWCListTabledescription">
					Xslt调用的样式表文件
				</td>
			</tr>
			<tr>
				<td class="SWCListTableField">
					Web服务器控件默认启用配置版本：
				</td>
				<td class="SWCListTableValue">
					<xsl:value-of select="swc:widgateDefaultVersion"/>
				</td>
				<td class="SWCListTabledescription">
					默认为1  优先判断主版本号 如果无法取出定义则取主版本定义
				</td>
			</tr>
			<tr>
				<td class="SWCListTableField">
					网站基本连接：
				</td>
				<td class="SWCListTableValue">
					<xsl:value-of select="swc:webSiteUrl"/>
				</td>
				<td class="SWCListTabledescription">

				</td>
			</tr>


		</xsl:for-each>
	</xsl:template>



	<xsl:template name="_temp_IncludeFileList">
		<tr>
			<td colspan="3" class="SWCListTableTitle">
				==========JS链接系列【<xsl:value-of select="swc:includeFileList/@jsBaseUrl"/>】==========
			</td>
		</tr>

		<xsl:for-each select="swc:includeFileList/swc:js">
			<tr>
				<td class="SWCListTableField">
					<xsl:value-of select="@fileId"/>
				</td>
				<td class="SWCListTableValue">
							<xsl:value-of select="."/>
					
				</td>
				<td class="SWCListTabledescription">
					<xsl:value-of select="@Description"/>
				</td>
			</tr>
		</xsl:for-each>

		<tr>
			<td colspan="3" class="SWCListTableTitle">
				==========CSS链接系列【<xsl:value-of select="swc:includeFileList/@cssBaseUrl"/>】==========
			</td>
		</tr>
		<xsl:for-each select="swc:includeFileList/swc:css">
			<tr>
				<td class="SWCListTableField">
					<xsl:value-of select="@fileId"/>
				</td>
				<td class="SWCListTableValue">
					<xsl:value-of select="."/>
				</td>
				<td class="SWCListTabledescription">
					<xsl:value-of select="@Description"/>
				</td>
			</tr>
		</xsl:for-each>
	</xsl:template>


	<xsl:template name="_temp_widget">
		<xsl:for-each select="swc:widget/swc:config">
			<xsl:if test="swc:pageSerch">
				<xsl:call-template name="_temp_widget_config_pageSerch">
					<xsl:with-param name="_parm_FistVersion" select="concat(../@version,'.',./@version)"></xsl:with-param>
				</xsl:call-template>
			</xsl:if>
		</xsl:for-each>
	</xsl:template>

	<xsl:template name="_temp_widget_config_pageSerch" >
		<xsl:param name="_parm_FistVersion"></xsl:param>

		<div class="SWCListTableWidget">
			查询控件  <xsl:value-of select="$_parm_FistVersion"/>

		</div>
		<table class="SWCListTable" cellpadding="0" cellspacing="1" border="0">
			<tr>
				<th>配置名称</th>
				<th>配置值</th>
				<th>描述</th>
				<th>全版本号</th>
			</tr>

			<xsl:for-each select="swc:pageSerch">
				<tr>
					<td class="SWCListTableField">
						包含文件
					</td>
					<td>
						<xsl:value-of select="swc:includeFile"/>
					</td>
					<td class="SWCListTabledescription">
						用逗号分隔的包含文件列表
					</td>
					<td>
						<xsl:value-of select="$_parm_FistVersion"/>.<xsl:value-of select="@version"/>.<xsl:value-of select="swc:includeFile/@version"/>
					</td>
				</tr>
			</xsl:for-each>
		</table>
	</xsl:template>


	

</xsl:stylesheet>
