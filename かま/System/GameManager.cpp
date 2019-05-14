#include "main.h"


#include "GameManager.h"

SPtr<Object> CreateObject(const std::string& className);

void Object::ImGuiUpdate()
{
	// 本当はここに書いてはいけない
	ImGuiInputString("Name", m_Name);

	// 第３引数 : ドラッグしたときの速度を設定
	//ImGui::DragFloat3("Pos", m_mWorld.GetPos(), 0.01f);
	ImGuizmoEditTransform(m_mWorld, ShMgr.m_mView, ShMgr.m_mProj, nullptr);
}

void Object::Deserialize(const json11::Json& jsonObj)
{
	m_Name = jsonObj["Name"].string_value();

	if (jsonObj["Pos"].is_array()) {    // しばらくこれにしておく(いずれ消す)
		auto& pos = jsonObj["Pos"].array_items();
		//KdMatrix m;
		m_mWorld.CreateMove(
			(float)pos[0].number_value(),
			(float)pos[1].number_value(),
			(float)pos[2].number_value()
		);
	}

	m_mWorld.Deserialize(jsonObj["Matrix"]);
}

void Object::Serialize(json11::Json::object& outJson)
{
	// 名前
	outJson["Name"] = m_Name;

	// 行列
	json11::Json::array mat;
    mat.resize(16);
	outJson["Matrix"] = m_mWorld.Serialize();;


}

void GameManager::Init()
{
	// JSON用に読み込み
	// JSONファイルの内容を全て文字列として読み込む。 
	std::string str = KdLoadStringFile("data/StageData.json");
	// JSON解析 
	std::string err;
	json11::Json jsn = json11::Json::parse(str, err);
	if (err.size() > 0) {
		// 解析エラー errにエラー内容が文字列で入っている 
	}

	auto& ary = jsn["Objects"].array_items();
	for (auto&& jsonObj : ary) {
		auto& className = jsonObj["ClassName"].string_value();

		// オブジェクトを宣言
		SPtr<Object> p;
		p = CreateObject(className);

		// デシリアライズ
		p->Deserialize(jsonObj);

		AddObject(p);

		/*
		if (className == "StageObject") {
		// ステージ
		SPtr<StageObject> p = std::make_shared<StageObject>();
		p->SetModel(jsonObj["ModelFileName"].string_value());

		auto& pos = jsonObj["Pos"].array_items();
		KdMatrix m;
		m.CreateMove(
		(float)pos[0].number_value(),
		(float)pos[1].number_value(),
		(float)pos[2].number_value()
		);

		p->SetMatrix(m);

		p->SetName(jsonObj["Name"].string_value());

		gameManager.AddObject(p);
		}
		else if (className == "Chara") {
		// キャラ
		SPtr<Chara> c = std::make_shared<Chara>();
		c->SetModel(jsonObj["ModelFileName"].string_value());

		auto& pos = jsonObj["Pos"].array_items();
		KdMatrix m;
		m.CreateMove(
		(float)pos[0].number_value(),
		(float)pos[1].number_value(),
		(float)pos[2].number_value()
		);

		c->SetMatrix(m);

		c->SetHp(jsonObj["Hp"].int_value());

		c->SetName(jsonObj["Name"].string_value());

		gameManager.AddObject(c);
		}
		*/
	}
}

void GameManager::Run()
{
	//===================================================
	// 更新処理
	//===================================================

	for (auto&& obj : m_ObjectList) {
		obj->Update();
	}

	/*=============================================*/
	// EditorCamera処理
	/*=============================================*/
	m_Editor_Camera.Update();

	//===================================================
	// 描画処理
	//===================================================

	// カメラ
	// ビュー行列
	/*ShMgr.m_mView.CreateMove(0, 1, -5);
	ShMgr.m_mView.Inverse();

	// 射影行列
	ShMgr.m_mProj.CreatePerspectiveFovLH(60.0f, 1.0f, 0.01f, 1000.0f);

	// カメラ情報をシェーダへセット
	ShMgr.UpdateCamera();
	*/

	// カメラをセット
	m_Editor_Camera.SetToShader();

	// バックバッファをクリア(RGBAの順)
	KD3D.GetBackBuffer()->ClearRT({ 0,0,1,1 });

	// Zバッファをクリア
	KD3D.GetZBuffer()->ClearDepth();

	
	// オブジェクトを描画
	for (auto&& obj : m_ObjectList) {
		obj->Draw();
	}

	// ImGui開始  
	ImGui_ImplDX11_NewFrame();
	ImGui_ImplWin32_NewFrame();
	ImGui::NewFrame();

	// ImGuizmo
	ImGuizmo::BeginFrame();

	// ImGui Demo ウィンドウ表示 ※すごく参考になるウィンドウです。
	// imgui_demo.cpp参照。  
	ImGui::ShowDemoWindow(nullptr);

	ImGuiDraw();

	// GUI描画実行  
	ImGui::Render();
	ImGui_ImplDX11_RenderDrawData(ImGui::GetDrawData());


	// 内容を表示
	KD3D.GetSwapChain()->Present(0, 0);

}

void GameManager::ImGuiDraw()
{

	/*================================*/
	//  Objectウィンドウ作成
	/*================================*/

	// ウィンドウ開始 “WindowName”の部分は他のウィンドウと被らないようにすること 
	if (ImGui::Begin("Object List", nullptr)) {
		

		for (auto& obj : m_ObjectList) {
			// objのアドレスを識別IDとして利用する
			ImGui::PushID((int)obj.get());

			bool b_Selected = false;
			if (m_Editor_SelectObj.lock() == obj)b_Selected = true;

			// 現在選択されているオブジェクト
			if (ImGui::Selectable(obj->GetName().c_str(), b_Selected)) {
				m_Editor_SelectObj = obj;
			}

			// 右クリックポップアップメニュー
			if (ImGui::BeginPopupContextItem("Object PopUp Menu", 1)) {

				// Prefab化
				if (ImGui::Selectable("Save to Json File")) {
					// このオブジェクトをシリアライズ
					json11::Json::object serial;
					obj->Serialize(serial);

					// Jsonデータを文字列に変換
					json11::Json jsn = serial;
					KdSaveStringFile("test.json", jsn.dump());
				}

				// Prefab化
				if (ImGui::Selectable("Load from Json File")) {
					std::string strJson = KdLoadStringFile("test.json");
					// ここからは自分で考える(書いているのは答えではない)
					// JSON解析 
					std::string err;
					json11::Json jsn = json11::Json::parse(strJson, err);
					if (err.size() > 0) {
						// 解析エラー errにエラー内容が文字列で入っている 
					}

					auto& ary = jsn["Objects"].array_items();
					for (auto&& jsonObj : ary) {
						auto& className = jsonObj["ClassName"].string_value();

						// オブジェクトを宣言
						SPtr<Object> p;
						p = CreateObject(className);

						// デシリアライズ
						p->Deserialize(jsonObj);

						AddObject(p);
					}
				}

				if (ImGui::Selectable("Duplicate")) {
					// このオブジェクトをシリアライズ
					json11::Json::object serial;
					obj->Serialize(serial);

					// 新しいObject作成(本来はダメ)
					std::string className = serial["ClassName"].string_value();
					SPtr<Object> newObj = CreateObject(className);

					newObj->Deserialize(serial);

					AddObject(newObj);
				}
				
				// 必ず最後に呼ぶこと
				ImGui::EndPopup();
			}

			ImGui::PopID();
		}

	}
	ImGui::End();

	if (ImGui::Begin("Inspector", nullptr)) {
	    SPtr<Object> select = m_Editor_SelectObj.lock();

		// メモリがあるかどうか確認
		if (select) {
			select->ImGuiUpdate();
		}

	}

	ImGui::End();

	/*==================================================*/
	// ここに描画したいものを書くこと
	/*==================================================*/

	//// shift-jisの文字列 
	//std::string str = "日本語でるよ！";
	//// STLのfilesystemを使って変換 
	//ImGui::Button(filesystem::path(str).u8string().c_str());

	/*==================================================*/

	
}