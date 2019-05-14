#include "main.h"

#include "System\GameManager.h"

#include "Charactor.h"

void Chara::SetModel(const std::string& _modelfilename)
{
	// いったん解放
	m_Bone.Release();
	m_Anime.ResetTrack();

	m_modelFileName = _modelfilename;

	m_model = KDResFactory.GetGameModel(_modelfilename);

	if (m_model) {

		// m_model用のボーンに初期化する
		m_Bone.Init(*m_model);

		// BoneControllerとアニメータを接続する
		m_Bone.BindAnimator(m_Anime);

		// アニメーションをモデルに登録
		m_Anime.SetAnimationList(m_model->GetAnimeList());
		m_Anime.ChangeAnime("攻撃１", true);  //アニメ名、ループ
	}
}

void Chara::Update()
{
	// アニメーション進行
	m_Anime.Animation(1,0,nullptr);

	// 全ボーンを再計算
	m_Bone.CalcBoneMatrix();

	m_mWorld.Move_Local(0.0f, 0.0f, 0.01f);
}

void Chara::Draw()
{
	if (m_model == nullptr)return;

	// シェーダに行列をセット
	ShMgr.m_KdModelSh.SetWorld(m_mWorld);

	// シェーダで描画
	ShMgr.m_KdModelSh.DrawGameModel(*m_model, &m_Bone);
}

void Chara::ImGuiUpdate()
{
	// 継承元の関数を呼ぶ
	Object::ImGuiUpdate();

	ImGui::DragInt("Hp", &n_Hp, 1.0f);

	// 画像ファイル選択ボタン m_TexFilenameはstd::string型です。
	if (ImGuiResourceButton(u8"Model File", m_modelFileName, { ".xed" })) {
		SetModel(m_modelFileName);
	}
}

void Chara::Deserialize(const json11::Json& jsonObj) 
{
	Object::Deserialize(jsonObj);

	// モデルファイル名を読み込む
	SetModel(jsonObj["ModelFileName"].string_value());

	// HP
	n_Hp = jsonObj["Hp"].int_value();
}

void Chara::Serialize(json11::Json::object& outJson)
{
	Object::Serialize(outJson);

	outJson["ModelFileName"] = m_modelFileName;
	outJson["ClassName"] = "Chara";

	outJson["Hp"] = n_Hp;
}